using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetinGo.Common;
using MetinGo.Fight;
using MetinGo.Server.Entities;
using MetinGo.Server.Infrastructure.Database;
using MetinGo.Server.Infrastructure.Session;
using Microsoft.EntityFrameworkCore;

namespace MetinGo.Server.Services
{
    public class FightService : IFightService
    {
        private readonly IFightSimulator _simulator;
        private readonly ISessionManager _sessionManager;
        private readonly MetinGoDbContext _db;
        private readonly IMapper _mapper;
        private readonly IMonsterExpRewardCalculator _expCalculator;
        private readonly ILootGenerator _lootGenerator;

        public FightService(IFightSimulator simulator, ISessionManager sessionManager, MetinGoDbContext db, IMapper mapper, IMonsterExpRewardCalculator expCalculator, ILootGenerator lootGenerator)
        {
            _simulator = simulator;
            _sessionManager = sessionManager;
            _db = db;
            _mapper = mapper;
            _expCalculator = expCalculator;
            _lootGenerator = lootGenerator;
        }

        public async Task<Entities.Fight> Fight(Guid monsterId)
        {
            var monster = await _db.FindAsync<Monster>(monsterId);

            var character = _sessionManager.CurrentCharacter;
            var characterItems = _db.Entry(_sessionManager.CurrentCharacter).Collection(c => c.CharacterItems).Query().Include(c => c.Item).ToList();
            var result =_simulator.Fight(_mapper.Map<Common.Character>(character), _mapper.Map<Fight.Model.Monster>(monster), characterItems);

            Entities.Fight fight = null;

            if (result.PlayerWon)
            {
                var characterLootItems = await _lootGenerator.GenerateLoot(monster, character);

                fight = new Entities.Fight
                {
                    Character = character,
                    Monster = monster,
                    Experience = _expCalculator.GetWinExp(monster.MonsterType, character.Level, monster.Level),
                    PlayerWon = true,
                    Loot = characterLootItems
                };

                _db.Add(fight);
            }
            else
            {
                fight = new Entities.Fight
                {
                    Character = character,
                    Monster = monster,
                    Experience = _expCalculator.GetLoseExp(monster.MonsterType, character.Level, monster.Level),
                    PlayerWon = false
                };

                _db.Add(fight);
            }

            await _db.SaveChangesAsync();

            return fight;
        }
    }
}
