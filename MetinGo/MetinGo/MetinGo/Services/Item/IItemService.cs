using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MetinGo.ApiModel.EquippedItem;
using MetinGo.ViewModels.Item;

namespace MetinGo.Services.Item
{
    public interface IItemService
    {
        Task UpdateItems();
        Task UpdateCharacterItems();
        Task<EquipItemResponse> EquipItem(Guid characterItemId);
        Task<List<CharacterItemViewModel>> GetCharacterItems();
    }
}