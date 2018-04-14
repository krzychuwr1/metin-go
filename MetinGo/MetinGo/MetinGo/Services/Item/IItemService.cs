using System.Collections.Generic;
using System.Threading.Tasks;
using MetinGo.ViewModels.Item;

namespace MetinGo.Services.Item
{
    public interface IItemService
    {
        Task UpdateItems();
        Task UpdateCharacterItems();
        Task<List<CharacterItemViewModel>> GetCharacterItems();
    }
}