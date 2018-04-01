using System.Threading.Tasks;
using MetinGo.Server.Entities;

namespace MetinGo.Server.Services
{
	public interface IUserService
	{
		string GetHash(string password);
		Task<User> CreateUser(string username, string password);
		Task<User> LoginUser(string username, string password);
	}
}