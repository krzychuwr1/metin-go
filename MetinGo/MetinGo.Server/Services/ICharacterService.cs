using System;
using System.Collections.Generic;
using MetinGo.Server.Entities;

namespace MetinGo.Server.Services
{
	public interface ICharacterService
	{
		IEnumerable<Character> GetUserCharacters(Guid userId);
		Character CreateCharacter(Guid userId, string characterName);
	}
}