using FileUploadService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FileUploadService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _apiKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _key;

        public AuthController(IConfiguration configuration)
        {
            _apiKey = configuration.GetValue<string>("X-API-KEY") ?? string.Empty;
            _issuer = configuration.GetValue<string>("Jwt:Issuer") ?? string.Empty;
            _audience = configuration.GetValue<string>("Jwt:Audience") ?? string.Empty;
            _key = configuration.GetValue<string>("Jwt:Key") ?? string.Empty;
        }

        [HttpPost]
        public IActionResult GenerateJwt([FromHeader, Required] string apiKey, [FromQuery, Required] UserCredential user)
        {
            if (apiKey != _apiKey)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Name, user.Name),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email),
                 new Claim(ClaimTypes.Role, user.Roles)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            if (key.KeySize < 256)
            {
                // Ensure key size is at least 256 bits
                throw new ArgumentException("Key size must be at least 256 bits.");
            }
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble("8"));

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(new Response<string>() { Success = true, Message = "success", Data = token });
        }
    }
}
