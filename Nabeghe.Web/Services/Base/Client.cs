namespace Nabeghe.Web.Services.Base
{
    public partial class Client : IClient
    {
        public HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }
        }

        public Client(HttpClient httpClient)
        {
            _httpClient = httpClient;
            BaseUrl = _httpClient.BaseAddress.ToString();
        }
    }
}
