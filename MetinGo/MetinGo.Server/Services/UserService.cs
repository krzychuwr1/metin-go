using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MetinGo.Server.Entities;
using MetinGo.Server.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MetinGo.Server.Services
{
    public class UserService : IUserService
    {
        private readonly MetinGoDbContext _db;

		public UserService(MetinGoDbContext db)
		{
		    _db = db;
		}

		public async Task<User> LoginUser(string username, string password)
		{
			var hash = GetHash(password);
			return await _db.Users.FirstOrDefaultAsync(u => u.Name == username && u.PasswordHash == hash);
		}

		public async Task<User> CreateUser(string username, string password)
		{
			var hash = GetHash(password);
		    var user = new User {Name = username, PasswordHash = hash};
		    _db.Add(user);
			await _db.SaveChangesAsync();
		    return user;
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
