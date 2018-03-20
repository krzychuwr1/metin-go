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
		public IEnumerable<Character> Get()
		{
			var characters = _characterService.GetUserCharacters(_sessionManager.GetUser(HttpContext).Id);

			return characters.Select(c => new Character{Id = c.Id, Name = c.Name});
		}

		[HttpPost]
		[ServiceFilter(typeof(UserContextFilter))]
		public Character Post(CreateCharacterRequest request)
		{
			var character = _characterService.CreateCharacter(_sessionManager.GetUser(HttpContext).Id, request.Name);
			return new Character {Id = character.Id, Name = character.Name};
		}
	}
}
