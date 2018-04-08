using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetinGo.ApiModel;
using MetinGo.ApiModel.Character;
using MetinGo.ApiModel.Login;
using MetinGo.Server.Infrastructure.Filters;
using MetinGo.Server.Infrastructure.Session;
using MetinGo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetinGo.Server.Controllers
{

	[Route("api/" + Endpoints.Character)]
	public class CharacterController : Controller
	{
		private readonly IUserService _userService;
		private readonly ISessionManager _sessionManager;
		private readonly ICharacterService _characterService;
	    private readonly IMapper _mapper;

	    public CharacterController(IUserService userService, ISessionManager sessionManager, ICharacterService characterService, IMapper mapper)
		{
			_userService = userService;
			_sessionManager = sessionManager;
			_characterService = characterService;
		    _mapper = mapper;
		}

		[HttpGet]
		[ServiceFilter(typeof(UserContextFilter))]
		public IActionResult Get()
		{
		    var characters = _characterService.GetCurrentUserCharacters();

		    var charactersMapped = _mapper.Map<List<Character>>(characters);

			return Ok(charactersMapped);
		}

		[HttpPost]
		[ServiceFilter(typeof(UserContextFilter))]
		public async Task<Character> Post([FromBody]CreateCharacterRequest request)
		{
			var character = await _characterService.CreateCharacter(_sessionManager.CurrentUser.Id, request.Name);
		    return _mapper.Map<Character>(character);
		}
	}
}
