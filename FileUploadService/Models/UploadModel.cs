namespace FileUploadService.Models
{
    public class Response<T>
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }
    }
    public class UploadModel
    {
        public List<string> FileName { get; set; }
        public string? ToEmail { get; set; }
        public string? ToEmailBody { get; set; }
        public UploadModel()
        {
            FileName = new List<string>();
        }
    }

}
