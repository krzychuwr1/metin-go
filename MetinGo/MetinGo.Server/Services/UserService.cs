using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MetinGo.Server.Entities;
using MetinGo.Server.Infrastructure.Database;

namespace MetinGo.Server.Services
{
    public class UserService : IUserService
	{
		private readonly IRepository<User> _userRepository;

		public UserService(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		public User LoginUser(string username, string password)
		{
			var hash = GetHash(password);
			return _userRepository.FindBy(u => u.Name == username && u.PasswordHash == hash).First();
		}

		public User CreateUser(string username, string password)
		{
			var hash = GetHash(password);
			_userRepository.Add(new User{Name=username, PasswordHash = hash});
			_userRepository.Save();
			return _userRepository.FindBy(u => u.Name == username).First();
		}

	    public string GetHash(string password)
	    {
			using (var sha256 = SHA256.Create())
			{
				var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));  
				return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
			}
		}
    }
}
