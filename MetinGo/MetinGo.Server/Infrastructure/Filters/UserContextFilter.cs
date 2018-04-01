using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.Server.Infrastructure.Session;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MetinGo.Server.Infrastructure.Filters
{
	public class UserContextFilter : IAsyncActionFilter
	{
		private readonly ISessionManager _sessionManager;

		public UserContextFilter(ISessionManager sessionManager)
		{
			_sessionManager = sessionManager;
		}

	    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	    {
	        if (context.HttpContext.Request.Headers.TryGetValue(RequestHeaders.UserId, out var userId))
	        {
	            await _sessionManager.SetUser(Guid.Parse(userId.First()), context.HttpContext);
	        }
	        await next();
	    }
	}
}
