using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public CharacterController(IUserService userService, ISessionManager sessionManager, ICharacterService characterService)
		{
			_userService = userService;
			_sessionManager = sessionManager;
			_characterService = characterService;
		}

		[HttpGet]
		[ServiceFilter(typeof(UserContextFilter))]
		public IActionResult Get()
		{
			var characters = _characterService.GetCurrentUserCharacters();

			return Ok(characters.Select(c => new Character{Id = c.Id, Name = c.Name}).ToList());
		}

		[HttpPost]
		[ServiceFilter(typeof(UserContextFilter))]
		public async Task<Character> Post([FromBody]CreateCharacterRequest request)
		{
			var character = await _characterService.CreateCharacter(_sessionManager.CurrentUser.Id, request.Name);
			return new Character {Id = character.Id, Name = character.Name};
		}
	}
}
