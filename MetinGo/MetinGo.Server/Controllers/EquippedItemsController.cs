using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Character;
using MetinGo.ApiModel.CharacterStats;
using MetinGo.ApiModel.EquippedItem;
using MetinGo.Server.Infrastructure.Database;
using MetinGo.Server.Infrastructure.Filters;
using MetinGo.Server.Infrastructure.Session;
using MetinGo.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetinGo.Server.Controllers
{
    [Route("api/" + Endpoints.EquippedItems)]
    public class EquippedItemsController : Controller
    {
        private readonly ISessionManager _sessionManager;
        private readonly IMapper _mapper;
        private readonly MetinGoDbContext _db;

        public EquippedItemsController(ISessionManager sessionManager, IMapper mapper, MetinGoDbContext db)
        {
            _sessionManager = sessionManager;
            _mapper = mapper;
            _db = db;
        }

        [HttpPost]
        [ServiceFilter(typeof(UserContextFilter))]
        [ServiceFilter(typeof(CharacterContextFilter))]
        public async Task<IActionResult> Post([FromBody]EquipItemRequest request)
        {
            var item = await _db.CharacterItems.FindAsync(request.CharacterItemId);
            var (equippedItemId, unequippedItemId) = await item.Equip(_db);
            await _db.SaveChangesAsync();
            return Ok(new EquipItemResponse{CharacterItemId = equippedItemId, UnequippedCharacterItemId = unequippedItemId});
        }
    }
}
