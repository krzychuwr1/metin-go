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

namespace MetinGo.Server.Controllers
{
    [Route("api/" + Endpoints.CharacterStats)]
    public class CharacterStatsController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISessionManager _sessionManager;
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;
        private readonly MetinGoDbContext _db;

        public CharacterStatsController(IUserService userService, ISessionManager sessionManager, ICharacterService characterService, IMapper mapper, MetinGoDbContext db)
        {
            _userService = userService;
            _sessionManager = sessionManager;
            _characterService = characterService;
            _mapper = mapper;
            _db = db;
        }

        [HttpPost]
        [ServiceFilter(typeof(UserContextFilter))]
        [ServiceFilter(typeof(CharacterContextFilter))]
        public async Task<Character> Post([FromBody]IncreaseStatsRequest request)
        {
            _sessionManager.CurrentCharacter.IncreaseStats(request.Attack, request.Defence, request.HP);
            await _db.SaveChangesAsync();
            return _mapper.Map<Character>(_sessionManager.CurrentCharacter);
        }
    }
}
