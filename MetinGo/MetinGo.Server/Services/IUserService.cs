using MetinGo.Server.Entities;

namespace MetinGo.Server.Services
{
	public interface IUserService
	{
		string GetHash(string password);
		User CreateUser(string username, string password);
		User LoginUser(string username, string password);
	}
}