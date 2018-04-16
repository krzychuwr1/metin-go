using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Character;
using MetinGo.ApiModel.Monster;
using MetinGo.Server.Infrastructure.Filters;
using MetinGo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetinGo.Server.Controllers
{
    [Route("api/" + Endpoints.Monster)]
    public class MonsterController : Controller
    {
        private readonly IMonsterService _monsterService;
        private readonly IMapper _mapper;

        public MonsterController(IMonsterService monsterService, IMapper mapper)
        {
            _monsterService = monsterService;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(UserContextFilter))]
        [ServiceFilter(typeof(CharacterContextFilter))]
        [ServiceFilter(typeof(PositionContextFilter))]
        public async Task<IActionResult> Get()
        {
            var monsters = await _monsterService.GetOrGenerateNearbyMonsters();
            var apiMonsters = _mapper.Map<List<Monster>>(monsters);
            return Ok(apiMonsters);
        }
    }
}
