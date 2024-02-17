using FileUploadServer.Models;
using FileUploadService.Models;

namespace FileUploadService.Services.Interface
{
    public interface IUploadService
    {
        Task<Response<UploadModel>> UploadFileServiceAsync(UploadRequest uploadRequest);
    }
}
