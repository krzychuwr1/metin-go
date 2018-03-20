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
	    private readonly IRepository<User> _userRepository;
	    private readonly IRepository<Character> _characteRepository;

	    public SessionManager(IRepository<User> userRepository, IRepository<Character> characteRepository)
	    {
		    _userRepository = userRepository;
		    _characteRepository = characteRepository;
	    }

	    public void SetUser(Guid userId, HttpContext context)
	    {
		    var user = _userRepository.FindBy(u => u.Id == userId).First();
			context.Items.Add(nameof(User), user);
	    }

	    public User GetUser(HttpContext context) => context.Items[nameof(User)] as User;

	    public void SetCharacter(Guid characterId, HttpContext context)
	    {
		    var character = _characteRepository.FindBy(u => u.Id == characterId).First();
		    context.Items.Add(nameof(Character), character);
	    }

	    public Character GetCharacter(HttpContext context) => context.Items[nameof(Character)] as Character;
	}
}
