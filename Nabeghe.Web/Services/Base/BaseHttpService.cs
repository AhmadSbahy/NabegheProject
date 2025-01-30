
using System.Net.Http.Headers;
using Nabeghe.Web.Contracts;

namespace Nabeghe.Web.Services.Base
{
    public class BaseHttpService
    {
        protected readonly IClient _client;
        protected readonly ILocalStorageService _localStorage;

        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        protected Response<Guid> ConvertApiException<Guid>(ApiException exception)
        {
            switch (exception.StatusCode)
            {
                case 400:
                    return new Response<Guid> { Message = "Validation Errors have occurred", ValidationErrors = exception.Response, IsSuccess = false };
                case 404:
                    return new Response<Guid> { Message = "Not Found ...", IsSuccess = false };
                default:
                    return new Response<Guid> { Message = "Something went wrong, try again ...", IsSuccess = false };
            }
        }
        protected void AddBearerToken()
        {
            if (_localStorage.IsExists("token"))
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _localStorage.GetStorageValue<string>("token"));
            }
        }
    }
}
