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
    [Route("api/" + Endpoints.NearbyCharacters)]
    public class NearbyCharactersController : Controller
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public NearbyCharactersController(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(UserContextFilter))]
        [ServiceFilter(typeof(CharacterContextFilter))]
        [ServiceFilter(typeof(PositionContextFilter))]
        public IActionResult Get()
        {
            var monsters = _characterService.GetNearbyCharacters();
            var apiMonsters = _mapper.Map<List<Character>>(monsters);
            return Ok(apiMonsters);
        }
    }
}
