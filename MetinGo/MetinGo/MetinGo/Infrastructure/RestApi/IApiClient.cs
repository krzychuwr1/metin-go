using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MetinGo.Infrastructure.RestApi
{
    public interface IApiClient
    {
	    Task Post<TRequest>(TRequest request, string endpoint);
	    Task<TResponse> Post<TRequest, TResponse>(TRequest request, string endpoint);
    }
}
