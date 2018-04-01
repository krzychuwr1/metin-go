using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.ApiModel;
using MetinGo.Server.Infrastructure.Session;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MetinGo.Server.Infrastructure.Filters
{
	public class PositionContextFilter : IAsyncActionFilter
	{
		private readonly ISessionManager _sessionManager;

		public PositionContextFilter(ISessionManager sessionManager)
		{
			_sessionManager = sessionManager;
		}
        
	    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	    {
	        if (context.HttpContext.Request.Headers.TryGetValue(RequestHeaders.Latitude, out var latitude) && context.HttpContext.Request.Headers.TryGetValue(RequestHeaders.Longitude, out var longitude))
	        {
	            await _sessionManager.SetPosition(double.Parse(latitude), double.Parse(longitude), context.HttpContext);
	        }
            await next();
	    }
	}
}
