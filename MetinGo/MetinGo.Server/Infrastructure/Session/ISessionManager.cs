using System;
using System.Threading.Tasks;
using MetinGo.Server.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace MetinGo.Server.Infrastructure.Session
{
	public interface ISessionManager
	{
		Character CurrentCharacter { get; }
		User CurrentUser { get; }
		Task SetCharacter(Guid characterId, HttpContext context);
		Task SetUser(Guid userId, HttpContext context);
	    Task SetPosition(double latitude, double longitude, HttpContext context);
	}
}