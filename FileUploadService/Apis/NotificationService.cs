using FileUploadService.Models;
using IdentityModel.Client;
using Newtonsoft.Json;
using System.Text;

namespace FileUploadServer.Apis
{
    public class NotificationService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        public NotificationService(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }
        private HttpClient NewHttpClient()
        {
            var client = _clientFactory.CreateClient();

            string _baseUri = _config.GetValue<string>("NotificationService:BaseUrl") ?? "";
            string _apiKey = _config.GetValue<string>("NotificationService:ApiKey") ?? "";

            #region for unit test
            //string _baseUri = "https://localhost:44357";
            //string _apiKey = "ZW1haWxzZXJ2ZXJzZXJ2aWNl";
            //client = new HttpClient();
            #endregion

            client.BaseAddress = new Uri(_baseUri);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-API-KEY", _apiKey);
            return client;
        }
        public async Task<HttpResponseMessage> SendEmailNotificationAsync(UploadModel data)
        {
            using (var client = NewHttpClient())
            {
                var dataSerialize = JsonConvert.SerializeObject(data);
                var response = await client.PostAsync($@"api/Email", new StringContent(dataSerialize, Encoding.UTF8, "application/json"));
                return response;
            }
        }
    }
}
