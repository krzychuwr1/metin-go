using System.Collections.Generic;
using System.Threading.Tasks;
using MetinGo.Server.Entities;

namespace MetinGo.Server.Services
{
    public interface ILootGenerator
    {
        Task<List<CharacterItem>> GenerateLoot(Monster monster, Character character);
    }
}