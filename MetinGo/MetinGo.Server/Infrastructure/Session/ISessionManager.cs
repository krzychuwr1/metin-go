using System;
using MetinGo.Server.Entities;
using Microsoft.AspNetCore.Http;

namespace MetinGo.Server.Infrastructure.Session
{
	public interface ISessionManager
	{
		Character GetCharacter(HttpContext context);
		User GetUser(HttpContext context);
		void SetCharacter(Guid characterId, HttpContext context);
		void SetUser(Guid userId, HttpContext context);
	}
}