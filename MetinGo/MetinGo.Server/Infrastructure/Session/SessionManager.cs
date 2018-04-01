using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.Server.Entities;
using MetinGo.Server.Infrastructure.Database;
using Microsoft.AspNetCore.Http;

namespace MetinGo.Server.Infrastructure.Session
{
    public class SessionManager : ISessionManager
	{
	    private readonly MetinGoDbContext _db;
	    private readonly IHttpContextAccessor _httpContextAccessor;

	    public SessionManager(MetinGoDbContext db, IHttpContextAccessor httpContextAccessor)
	    {
	        _db = db;
	        _httpContextAccessor = httpContextAccessor;
	    }

	    public async Task SetUser(Guid userId, HttpContext context)
	    {
	        var user = await _db.FindAsync<User>(userId);
			context.Items.Add(nameof(Entities.User), user);
	    }

	    public async Task SetPosition(double latitude, double longitude, HttpContext context)
	    {
	        if (CurrentCharacter == null)
	            throw new ApplicationException("Trying to set position without character context");

	        CurrentCharacter.Latitude = latitude;
	        CurrentCharacter.Longitude = longitude;
	    }

	    public User CurrentUser => _httpContextAccessor.HttpContext.Items[nameof(Entities.User)] as User;

	    public async Task SetCharacter(Guid characterId, HttpContext context)
	    {
	        var character = await _db.FindAsync<Character>(characterId);
		    context.Items.Add(nameof(Character), character);
	    }

	    public Character CurrentCharacter => _httpContextAccessor.HttpContext.Items[nameof(Character)] as Character;
	}
}
