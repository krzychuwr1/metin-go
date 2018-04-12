using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Character;
using MetinGo.ApiModel.CharacterStats;
using MetinGo.Server.Infrastructure.Database;
using MetinGo.Server.Infrastructure.Filters;
using MetinGo.Server.Infrastructure.Session;
using MetinGo.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetinGo.Server.Controllers
{
    [Route("api/" + Endpoints.CharacterItems)]
    public class CharacterItemsController : Controller
    {
        private readonly ISessionManager _sessionManager;
        private readonly IMapper _mapper;
        private readonly MetinGoDbContext _db;

        public CharacterItemsController(ISessionManager sessionManager, IMapper mapper, MetinGoDbContext db)
        {
            _sessionManager = sessionManager;
            _mapper = mapper;
            _db = db;
        }

        [HttpGet]
        [ServiceFilter(typeof(UserContextFilter))]
        [ServiceFilter(typeof(CharacterContextFilter))]
        public async Task<List<ApiModel.Item.Item>> Get()
        {
            var items = await _db.CharacterItems.Where(i => i.CharacterId == _sessionManager.CurrentCharacter.Id).Select(i => i.Item).ToListAsync();
            return _mapper.Map<List<ApiModel.Item.Item>>(items);
        }
    }
}
