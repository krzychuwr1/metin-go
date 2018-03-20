using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetinGo.Server.Entities;
using MetinGo.Server.Infrastructure.Database;

namespace MetinGo.Server.Services
{
    public class CharacterService : ICharacterService
	{
	    private readonly IRepository<Character> _characterRepository;

	    public CharacterService(IRepository<Character> characterRepository)
	    {
		    _characterRepository = characterRepository;
	    }

	    public IEnumerable<Character> GetUserCharacters(Guid userId) => _characterRepository.FindBy(c => c.UserId == userId);
		public Character CreateCharacter(Guid userId, string characterName)
		{
			var character = new Character{Name = characterName, UserId = userId};
			_characterRepository.Add(character);
			_characterRepository.Save();
			return character;
		}
	}
}
