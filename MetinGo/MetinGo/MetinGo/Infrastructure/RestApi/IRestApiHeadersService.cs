using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MetinGo.Infrastructure.RestApi
{
    public interface IRestApiHeadersService
    {
        void AddHeaders(HttpClient client);
    }
}
