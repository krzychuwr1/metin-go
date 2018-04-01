using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.Server.Entities;

namespace MetinGo.Server.Services
{
    public interface IMonsterService
    {
        Task<IEnumerable<Monster>> GetOrGenerateNearbyMonsters();
    }
}
