using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MetinGo.Server.Entities;

namespace MetinGo.Server.Services
{
	public interface ICharacterService
	{
		IEnumerable<Character> GetCurrentUserCharacters();
		Task<Character> CreateCharacter(Guid userId, string characterName);
	    IEnumerable<Character> GetNearbyCharacters();
	}
}