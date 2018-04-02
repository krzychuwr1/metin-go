using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetinGo.Fight;
using MetinGo.Server.Entities;
using MetinGo.Server.Infrastructure.Database;
using MetinGo.Server.Infrastructure.Session;
using Character = MetinGo.Fight.Model.Character;

namespace MetinGo.Server.Services
{
    public class FightService : IFightService
    {
        private readonly IFightSimulator _simulator;
        private readonly ISessionManager _sessionManager;
        private readonly MetinGoDbContext _db;
        private readonly IMapper _mapper;

        public FightService(IFightSimulator simulator, ISessionManager sessionManager, MetinGoDbContext db, IMapper mapper)
        {
            _simulator = simulator;
            _sessionManager = sessionManager;
            _db = db;
            _mapper = mapper;
        }

        public async Task<Entities.Fight> Fight(Guid monsterId)
        {
            var monster = await _db.FindAsync<Monster>(monsterId);

            var result =_simulator.Fight(_mapper.Map<Character>(_sessionManager.CurrentCharacter), _mapper.Map<Fight.Model.Monster>(monster));

            Entities.Fight fight = null;

            if (result.PlayerWon)
            {
                fight = new Entities.Fight
                {
                    Character = _sessionManager.CurrentCharacter,
                    Monster = monster,
                    Experience = 10,
                    PlayerWon = true
                };

                _db.Add(fight);
            }
            else
            {
                fight = new Entities.Fight
                {
                    Character = _sessionManager.CurrentCharacter,
                    Monster = monster,
                    Experience = -5,
                    PlayerWon = false
                };

                _db.Add(fight);
            }

            await _db.SaveChangesAsync();

            return fight;
        }
    }
}
