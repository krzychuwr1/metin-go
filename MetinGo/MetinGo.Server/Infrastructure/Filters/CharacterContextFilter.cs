using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.Server.Infrastructure.Session;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MetinGo.Server.Infrastructure.Filters
{
	public class CharacterContextFilter : IActionFilter
	{
		private readonly ISessionManager _sessionManager;

		public CharacterContextFilter(ISessionManager sessionManager)
		{
			_sessionManager = sessionManager;
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			if (context.HttpContext.Request.Headers.TryGetValue(RequestHeaders.CharacterId, out var characterId))
			{
				_sessionManager.SetCharacter(Guid.Parse(characterId.First()), context.HttpContext);
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			
		}
	}
}
