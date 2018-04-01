using System.Net.Http;
using MetinGo.ApiModel;
using MetinGo.Infrastructure.Session;

namespace MetinGo.Infrastructure.RestApi
{
    public class RestApiHeadersService : IRestApiHeadersService
    {
        private readonly ISessionManager _sessionManager;

        public RestApiHeadersService(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }


        public void AddHeaders(HttpClient client)
        {
            if (_sessionManager.Latitude != null)
                client.DefaultRequestHeaders.Add(RequestHeaders.Latitude, _sessionManager.Latitude.ToString());

            if (_sessionManager.Longitude != null)
                client.DefaultRequestHeaders.Add(RequestHeaders.Longitude, _sessionManager.Longitude.ToString());

            if (_sessionManager.User?.Id != null)
                client.DefaultRequestHeaders.Add(RequestHeaders.UserId, _sessionManager.User.Id.ToString());

            if (_sessionManager.Character?.Id != null)
                client.DefaultRequestHeaders.Add(RequestHeaders.CharacterId, _sessionManager.Character.Id.ToString());
        }
    }
}