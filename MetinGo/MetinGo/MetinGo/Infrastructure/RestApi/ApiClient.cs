using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MetinGo.Infrastructure.RestApi
{
    public class ApiClient
    {
	    public async Task Post<TRequest>(TRequest request)
	    {
		    using (var client = new HttpClient())
		    {
			    var json = JsonConvert.SerializeObject(request);
			    var content = new StringContent(json, Encoding.UTF8, "application/json");

			    var response  = await client.PostAsync("http://192.168.0.104/MetinGoServer/api/Registration", content);
		    }
	    }
    }
}
