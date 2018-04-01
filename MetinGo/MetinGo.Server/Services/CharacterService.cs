using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.Server.Entities;
using MetinGo.Server.Infrastructure.Database;
using MetinGo.Server.Infrastructure.Session;

namespace MetinGo.Server.Services
{
    public class CharacterService : ICharacterService
	{
	    private readonly MetinGoDbContext _db;
	    private readonly ISessionManager _sessionManager;

	    public CharacterService(MetinGoDbContext db, ISessionManager sessionManager)
	    {
	        _db = db;
	        _sessionManager = sessionManager;
	    }

	    public IEnumerable<Character> GetCurrentUserCharacters() => _db.Characters.Where(c => c.UserId == _sessionManager.CurrentUser.Id);
		public async Task<Character> CreateCharacter(Guid userId, string characterName)
		{
			var character = new Character{Name = characterName, UserId = userId};
		    _db.Add(character);
		    await _db.SaveChangesAsync();
			return character;
		}
	}
}
