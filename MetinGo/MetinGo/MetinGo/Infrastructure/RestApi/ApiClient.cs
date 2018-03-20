using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MetinGo.Infrastructure.RestApi
{
    public class ApiClient : IApiClient
    {
	    public async Task Post<TRequest>(TRequest request, string endpoint)
	    {
		    if (request == null) throw new ArgumentNullException(nameof(request));
		    if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));

			using (var client = new HttpClient())
		    {
			    var json = JsonConvert.SerializeObject(request);
			    var content = new StringContent(json, Encoding.UTF8, "application/json");
			    await client.PostAsync("http://192.168.0.104/MetinGoServer/api/" + endpoint, content);
		    }
	    }

	    public async Task<TResponse> Post<TRequest, TResponse>(TRequest request, string endpoint)
	    {
		    if (request == null) throw new ArgumentNullException(nameof(request));
		    if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));

		    using (var client = new HttpClient())
		    {
			    var json = JsonConvert.SerializeObject(request);
			    var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
			    var response = await client.PostAsync("http://192.168.0.104/MetinGoServer/api/" + endpoint, requestContent);
			    var responseContent = await response.Content.ReadAsStringAsync();
			    var responseDeserialized = JsonConvert.DeserializeObject<TResponse>(responseContent);
			    return responseDeserialized;
		    }
	    }
	}
}
