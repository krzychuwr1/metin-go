using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.Common;
using MetinGo.Server.Entities;
using MetinGo.Server.Infrastructure.Database;
using MetinGo.Server.Infrastructure.Session;
using Microsoft.EntityFrameworkCore;
using Troschuetz.Random;
using Character = MetinGo.Server.Entities.Character;

namespace MetinGo.Server.Services
{
    public class MonsterService : IMonsterService
    {
        private readonly MetinGoDbContext _db;
        private readonly ISessionManager _sessionManager;
        private readonly TRandom _random = new TRandom();

        public MonsterService(MetinGoDbContext db, ISessionManager sessionManager)
        {
            _db = db;
            _sessionManager = sessionManager;
        }

        public async Task<IEnumerable<Monster>> GetOrGenerateNearbyMonsters()
        {
            var character = _sessionManager.CurrentCharacter;
            var monstersCount = await _db.Monsters.Where(m => 
                    Math.Abs(m.Latitude - character.Latitude) < 0.001 && 
                    Math.Abs(m.Longitude - character.Longitude) < 0.0015 && 
                    Math.Abs(m.Level - character.Level) < 5 && m.IsAlive)
                .CountAsync();

            var monstersToGenerateAmount = Math.Max(0, 5 - monstersCount);

            await GenerateMonsters(monstersToGenerateAmount, character);

            return await _db.Monsters.Where(m =>
                Math.Abs(m.Latitude - character.Latitude) < 0.001 &&
                Math.Abs(m.Longitude - character.Longitude) < 0.0015 &&
                Math.Abs(m.Level - character.Level) < 5 && m.IsAlive).ToListAsync();
        }

        private async Task<IEnumerable<Monster>> GenerateMonsters(int amount, Character character)
        {
            var monsters = new List<Monster>();
            var monsterTypesCount = Enum.GetNames(typeof(MonsterType)).Length;
            for (var i = 0; i < amount; i++)
            {
                var monster = new Monster
                {
                    Level  = Math.Max(1, character.Level + _random.Next(-2, 4)),
                    Longitude = character.Longitude + _random.NextDouble(-0.0015, 0.0015),
                    Latitude = character.Latitude + _random.NextDouble(-0.001, 0.001),
                    IsAlive = true,
                    MonsterType = (MonsterType)_random.Next(1, monsterTypesCount + 1)
                };
                monsters.Add(monster);
            }

            _db.AddRange(monsters);
            await _db.SaveChangesAsync();
            return monsters;
        }
    }
}