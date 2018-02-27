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
    public class AuthAdminController : Controller
    {
        private readonly IAuthRepository _adminRepo;
        private readonly IConfiguration _adminConfigRepo;
        public AuthAdminController(IAuthRepository repo, IConfiguration config)
        {
            _adminConfigRepo = config;
            _adminRepo = repo;
        }

        //POST Register Patient
        [Authorize (Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterPatient([FromBody]PatientRegister patientRegister)
        {
            if(!string.IsNullOrEmpty(patientRegister.Name))
            patientRegister.Name = patientRegister.Name.ToLower();

            if(await _adminRepo.PatientExists(patientRegister.Name))
            ModelState.AddModelError("Name", "Имя пользователя уже используется");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);            

            var PatientToCreate = new Patient
            {
                Role = "admin",
                Name = patientRegister.Name,
                FamilyName = patientRegister.FamilyName,
                MiddleName = patientRegister.MiddleName,
                Birthdate = patientRegister.Birthdate
            };

            var createPatient = await _adminRepo.RegisterPatient(PatientToCreate, patientRegister.Password);

            return StatusCode(201);
        }

        //POST Register Staff
        [Authorize (Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterStaff([FromBody]StaffRegister staffRegister)
        {
            if(!string.IsNullOrEmpty(staffRegister.Name))
            staffRegister.Name = staffRegister.Name.ToLower();

            if(await _adminRepo.StaffExists(staffRegister.Name))
            ModelState.AddModelError("Name", "Имя пользователя уже используется");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);            

            var StaffToCreate = new Staff
            {
                Role = "staff",
                Name = staffRegister.Name,
                FamilyName = staffRegister.FamilyName,
                MiddleName = staffRegister.MiddleName,
                Birthdate = staffRegister.Birthdate
            };

            var createStaff = await _adminRepo.RegisterStaff(StaffToCreate, staffRegister.Password);

            return StatusCode(201);
        }


        [Authorize (Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin([FromBody]StaffRegister staffRegister)
        {
            if(!string.IsNullOrEmpty(staffRegister.Name))
            staffRegister.Name = staffRegister.Name.ToLower();

            if(await _adminRepo.StaffExists(staffRegister.Name))
            ModelState.AddModelError("Name", "Имя пользователя уже используется");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);            

            var AdminToCreate = new Admin
            {
                Role = "admin",
                Name = staffRegister.Name,
                FamilyName = staffRegister.FamilyName,
                MiddleName = staffRegister.MiddleName,
                Birthdate = staffRegister.Birthdate
            };

            var createAdmin = await _adminRepo.RegisterAdmin(AdminToCreate, staffRegister.Password);

            return StatusCode(201);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]SharedLogin sharedLogin)
        {
            var adminFromRepo = await _adminRepo.LoginAdmin(sharedLogin.Name.ToLower(), sharedLogin.Password);
            
            if(adminFromRepo == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_adminConfigRepo.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, adminFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, adminFromRepo.Name),
                    new Claim(ClaimTypes.Role, adminFromRepo.Role)
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