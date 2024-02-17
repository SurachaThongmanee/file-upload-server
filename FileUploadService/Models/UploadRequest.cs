namespace FileUploadServer.Models
{
    public class UploadRequest()
    {
        public List<IFormFile>? FilesData { get; set; }
        public string? ToEmail { get; set; }
        public string? ToEmailBody { get; set; }
    }
}
