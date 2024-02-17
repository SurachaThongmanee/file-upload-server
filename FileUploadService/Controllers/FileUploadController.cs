using FileUploadServer.Models;
using FileUploadService.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FileUploadController : ControllerBase
    {
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoic3VyYWNoYW1heCIsIm5hbWVpZCI6IjEiLCJlbWFpbCI6InN1cmFjaGF0aG9uZ21hbmVlQGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE3MDg2MDE1NDAsImlzcyI6InN1cmFjaGEiLCJhdWQiOiJzdXJhY2hhdGhvbmdtYW5lZSJ9.hrSNqSStmcxR29HaaolOnumHYV6FibH-gXSRg80IZqE
        private readonly IUploadService _uploadService;
        public FileUploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UploadFile(UploadRequest requestData)
        {
            if (requestData == null || requestData?.FilesData?.Count == 0 || requestData?.FilesData is null)
                return BadRequest("Not founded the file.");

            var response = await _uploadService.UploadFileServiceAsync(requestData ?? new());
            return response.Success ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
