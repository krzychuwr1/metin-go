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
        private readonly IRestApiHeadersService _headersService;

        public ApiClient(IRestApiHeadersService headersService)
        {
            _headersService = headersService;
        }

	    public async Task Post<TRequest>(TRequest request, string endpoint)
	    {
		    if (request == null) throw new ArgumentNullException(nameof(request));
		    if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));

			using (var client = new HttpClient())
		    {
                _headersService.AddHeaders(client);
			    var json = JsonConvert.SerializeObject(request);
			    var content = new StringContent(json, Encoding.UTF8, "application/json");
			    await client.PostAsync("http://192.168.1.57/MetinGo.Server/api/" + endpoint, content);
		    }
	    }

	    public async Task<TResponse> Post<TRequest, TResponse>(TRequest request, string endpoint)
	    {
		    if (request == null) throw new ArgumentNullException(nameof(request));
		    if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));

		    using (var client = new HttpClient())
		    {
		        _headersService.AddHeaders(client);
                var json = JsonConvert.SerializeObject(request);
			    var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
			    var response = await client.PostAsync("http://192.168.1.57/MetinGo.Server/api/" + endpoint, requestContent);
			    var responseContent = await response.Content.ReadAsStringAsync();
			    var responseDeserialized = JsonConvert.DeserializeObject<TResponse>(responseContent);
			    return responseDeserialized;
		    }
	    }

        public async Task<TResponse> Get<TResponse>(string endpoint)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));

            using (var client = new HttpClient())
            {
                _headersService.AddHeaders(client);
                var response = await client.GetAsync("http://192.168.1.57/MetinGo.Server/api/" + endpoint);
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseDeserialized = JsonConvert.DeserializeObject<TResponse>(responseContent);
                return responseDeserialized;
            }
        }

        public async Task<TResponse> Get<TRequest, TResponse>(TRequest request, string endpoint)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));

            using (var client = new HttpClient())
            {
                var queryString = request.GetQueryString();
                _headersService.AddHeaders(client);
                var json = JsonConvert.SerializeObject(request);
                var response = await client.GetAsync("http://192.168.1.57/MetinGo.Server/api/" + endpoint + queryString);
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseDeserialized = JsonConvert.DeserializeObject<TResponse>(responseContent);
                return responseDeserialized;
            }
        }
    }
}
