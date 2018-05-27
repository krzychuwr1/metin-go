using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.Server.Entities;
using MetinGo.Server.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Troschuetz.Random;
using CharacterItem = MetinGo.ApiModel.Item.CharacterItem;

namespace MetinGo.Server.Services
{
    public class LootGenerator : ILootGenerator
    {
        private readonly MetinGoDbContext _db;
        private readonly Random _random;

        public LootGenerator(MetinGoDbContext db)
        {
            _db = db;
            _random = new Random();
        }

        public async Task<List<Entities.CharacterItem>> GenerateLoot(Monster monster, Character character)
        {
            var possibleLootItems = await _db.MonsterTypeLoots.Where(l => l.MonsterType == monster.MonsterType).Include(i => i.Item).ToListAsync();
            var generatedItems = new List<Entities.CharacterItem>();
             foreach (var item in possibleLootItems)
             {
                 var result = _random.Next(0, (int)1e6) / 1e6;
                 if (result < (double) item.Probability)
                {
                    generatedItems.Add(new Entities.CharacterItem {Item = item.Item, Level = monster.Level});
                }
             }

            return generatedItems;
        }
    }
}
