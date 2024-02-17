using FileUploadServer.Apis;
using FileUploadServer.Models;
using FileUploadService.Models;
using FileUploadService.Services.Interface;
using Newtonsoft.Json;

namespace FileUploadService.Services
{
    public class UploadService : IUploadService
    {
        private readonly string _uploadDirectory = "Uploads";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public UploadService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<Response<UploadModel>> UploadFileServiceAsync(UploadRequest uploadRequest)
        {
            try
            {
                if (!Directory.Exists(_uploadDirectory))
                    Directory.CreateDirectory(_uploadDirectory);

                List<string> fileNames = new List<string>();

                foreach (var file in uploadRequest.FilesData ?? new())
                {
                    var fileName = $"{Guid.NewGuid()}_{file.FileName}";

                    var filePath = Path.Combine(_uploadDirectory, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    fileNames.Add(fileName);
                }

                var payload = new UploadModel()
                {
                    FileName = fileNames,
                    ToEmail = uploadRequest.ToEmail,
                    ToEmailBody = uploadRequest.ToEmailBody,
                };

                var response = await SendEmailNotification(payload);

                return response;
            }
            catch (Exception e)
            {
                return new Response<UploadModel>()
                {
                    Success = false,
                    Message = $"failure: {e.Message}",
                    Data = new UploadModel()
                };
            }
        }

        public async Task<Response<UploadModel>> SendEmailNotification(UploadModel payload)
        {
            try
            {
                var emailApi = new NotificationService(_httpClientFactory, _config);
                var response = await emailApi.SendEmailNotificationAsync(payload);

                if (response.IsSuccessStatusCode)
                {
                    var content = JsonConvert.DeserializeObject<Response<UploadModel>>(await response.Content.ReadAsStringAsync()) ?? new Response<UploadModel>();
                    return content;
                }
                else
                {
                    return new Response<UploadModel>();
                }
            }
            catch (Exception e)
            {
                return new Response<UploadModel>()
                {
                    Success = false,
                    Message = $"failure: {e.Message}",
                    Data = null
                };
            }
        }
    }
}