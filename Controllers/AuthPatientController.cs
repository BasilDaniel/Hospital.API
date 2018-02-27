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
    // [Authorize]
    [Route("api/[controller]/[action]")]
    public class AuthPatientController : Controller
    {
         private readonly IAuthRepository _patientRepo;
        private readonly IConfiguration _patientConfigRepo;
        public AuthPatientController(IAuthRepository repo, IConfiguration config)
        {
            _patientConfigRepo = config;
            _patientRepo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]PatientRegister patientRegister)
        {
            if(!string.IsNullOrEmpty(patientRegister.Name))
            patientRegister.Name = patientRegister.Name.ToLower();

            // Check unique Name
            // if(await _patientRepo.PatientExists(patientRegister.Name)) 
            // ModelState.AddModelError("Name", "Имя пользователя уже используется");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);            

            var PatientToCreate = new Patient
            {
                Role = "patient",
                Name = patientRegister.Name,
                FamilyName = patientRegister.FamilyName,
                MiddleName = patientRegister.MiddleName,
                Birthdate = patientRegister.Birthdate
            };

            var createPatient = await _patientRepo.RegisterPatient(PatientToCreate, patientRegister.Password);

            return StatusCode(201);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]SharedLogin sharedLogin)
        {
            var patientFromRepo = await _patientRepo.LoginPatient(sharedLogin.Name.ToLower(), sharedLogin.Password);
            
            if(patientFromRepo == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_patientConfigRepo.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, patientFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, patientFromRepo.Name),
                    new Claim(ClaimTypes.Role, patientFromRepo.Role)
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