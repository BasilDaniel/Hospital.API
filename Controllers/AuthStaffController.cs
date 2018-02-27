using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hospital.API.Data;
using Hospital.API.Dtos;
using Hospital.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthStaffController : Controller
    {
        private readonly IAuthRepository _staffRepo;
        private readonly IConfiguration _staffConfigRepo;
        public AuthStaffController(IAuthRepository repo, IConfiguration config)
        {
            _staffConfigRepo = config;
            _staffRepo = repo;
        }       

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]SharedLogin sharedLogin)
        {
            var staffFromRepo = await _staffRepo.LoginStaff(sharedLogin.Name.ToLower(), sharedLogin.Password);
            
            if(staffFromRepo == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_staffConfigRepo.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, staffFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, staffFromRepo.Name),
                    new Claim(ClaimTypes.Role, staffFromRepo.Role)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { tokenString });
        }
    }
}