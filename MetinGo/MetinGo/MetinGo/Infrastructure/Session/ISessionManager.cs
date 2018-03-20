using System;
using System.Collections.Generic;
using System.Text;
using MetinGo.Models.User;
using Newtonsoft.Json;

namespace MetinGo.Infrastructure.Session
{
    public interface ISessionManager
    {
	    User User { get; set; }
    }

	public class SessionManager : ISessionManager
	{
		public User User
		{
			get => App.Current.Properties.TryGetValue(nameof(User), out var userObject)
				? JsonConvert.DeserializeObject<User>(userObject as string)
				: null;
			set
			{
				if (App.Current.Properties.ContainsKey(nameof(User)))
					App.Current.Properties.Remove(nameof(User));
				App.Current.Properties.Add(nameof(User), JsonConvert.SerializeObject(value));
			}
		}
	}
}
