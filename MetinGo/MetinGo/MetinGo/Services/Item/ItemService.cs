using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Character;
using MetinGo.ApiModel.EquippedItem;
using MetinGo.ApiModel.Item;
using MetinGo.Common;
using MetinGo.Infrastructure.RestApi;
using MetinGo.Infrastructure.Session;
using MetinGo.ViewModels.Item;
using Realms;

namespace MetinGo.Services.Item
{
    public class ItemService : IItemService
    {
        private readonly IApiClient _apiClient;
        private readonly ISessionManager _sessionManager;
        private readonly IItemWithLevelStatsCalculator _statsCalculator;

        public ItemService(IApiClient apiClient, ISessionManager sessionManager, IItemWithLevelStatsCalculator statsCalculator)
        {
            _apiClient = apiClient;
            _sessionManager = sessionManager;
            _statsCalculator = statsCalculator;
        }

        public async Task UpdateItems()
        {
            var items = await _apiClient.Get<List<ApiModel.Item.Item>>(Endpoints.Item);
            var db = Realm.GetInstance();
            var mobileItems = items.Select(i => new Models.Item.Item
            {
                Attack = i.Attack,
                Defence = i.Defence,
                Description = i.Description,
                Id = i.Id,
                ImagePath = i.ImagePath,
                ItemTypeId = (int)i.ItemType,
                Name = i.Name,
                RarityId = (int)i.Rarity,
                MaxHP = i.MaxHP,
                PerLevelAttack = i.PerLevelAttack,
                PerLevelDefence = i.PerLevelDefence,
                PerLevelMaxHP = i.PerLevelMaxHP
            }).ToList();
            await db.WriteAsync(r =>
            {
                foreach (var item in mobileItems)
                {
                    r.Add(item, update: true);
                }
            });
        }

        public async Task UpdateCharacterItems()
        {
            var characterItems = await _apiClient.Get<List<CharacterItem>>(Endpoints.CharacterItems);
            var db = Realm.GetInstance();
            var items = db.All<Models.Item.Item>().ToList();
            var mobileCharacterItems = characterItems.Select(i => new Models.Item.CharacterItem
            {
                CharacterId = _sessionManager.Character.Id.ToString(),
                Id = i.Id.ToString(),
                Item = items.Single(it => it.Id == i.ItemId),
                Level = i.Level,
                IsEquipped = i.IsEquipped
            }).ToList();
            db.Write(() =>
            {
                foreach (var item in mobileCharacterItems)
                {
                    db.Add(item, update: true);
                }
            });
        }

        public async Task<EquipItemResponse> EquipItem(Guid characterItemId)
        {
            return await _apiClient.Post<EquipItemRequest, EquipItemResponse>(new EquipItemRequest() {CharacterItemId = characterItemId}, Endpoints.EquippedItems);
        }

        public async Task<List<CharacterItemViewModel>> GetCharacterItems()
        {
            var db = Realm.GetInstance();
            var characterItems = db.All<Models.Item.CharacterItem>().ToList();
            var viewModels = characterItems.Select(i => new CharacterItemViewModel()
            {
                CharacterItem = i,
                ItemWithLevelStats = _statsCalculator.Calculate(i.Item, i.Level)
            }).ToList();
            return viewModels;
        }
    }
}
