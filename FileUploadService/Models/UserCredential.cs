namespace FileUploadService.Models
{
    public class UserCredential
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Roles { get; set; } = string.Empty;
    }
}
