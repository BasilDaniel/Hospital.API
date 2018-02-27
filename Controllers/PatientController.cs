using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.API.Data;
using Hospital.API.Dtos;
using Hospital.API.Helpers;
using Hospital.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.API.Controllers
{

    [Authorize(Roles = "patient")]
    [Route("api/[controller]/[action]")]
    public class PatientController : Controller
    {
        private readonly IDatingRepository _patientRepo;
        private readonly IMapper _patientMapper;
        public  PatientController(IDatingRepository repo, IMapper mapper)
        {
            _patientMapper = mapper;
            _patientRepo = repo;
        } 

        // GET Patient/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Patient(int id)
        { 
            var currentPatientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var patient = await _patientRepo.GetPatient(id);

            if (patient == null)
                return NotFound($"Could not find user with an ID of {id}");

            if (currentPatientId != patient.Id)
                return Unauthorized();

            var patientToReturn = _patientMapper.Map<PatientDetailed>(patient);

            return Ok(patientToReturn);
        }  

        // PUT Patient/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] PatientUpdate patientUpdate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentPatientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var patient = await _patientRepo.GetPatient(id);

            if (patient == null)
                return NotFound($"Пользователя с Id {id} не существует");

            if (currentPatientId != patient.Id)
                return Unauthorized();

            _patientMapper.Map(patientUpdate, patient);

            if(await _patientRepo.SaveAll())
                return NoContent();
                
            throw new Exception($"Пользователь {id} НЕ обновлен!");
        }   

        // GET Staffs
        [HttpGet]
        public async Task<IActionResult> Staffs(UserParams userParams)
        {
            var staffs = await _patientRepo.GetStaffs(userParams);
            var staffToReturn = _patientMapper.Map<IEnumerable<SharedStaffsList>>(staffs);
            Response.AddPagination(staffs.CurrentPage, staffs.PageSize, staffs.TotalCount, staffs.TotalPages);
            
            return Ok(staffToReturn);
        }

        // GET Staff/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Staff(int id)
        {
            var staff = await _patientRepo.GetStaff(id);
            var staffToReturn = _patientMapper.Map<PatientStaffDetailed>(staff);

            return Ok(staffToReturn);
        }

        // POST Appointment
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] PatientAppointmentCreate patientAppointmentCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentPatientId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if(patientAppointmentCreate.DateTime != null && 
                patientAppointmentCreate.DateTime > DateTime.Now && 
                patientAppointmentCreate.PatientId != null){

                if (currentPatientId != patientAppointmentCreate.PatientId)
                    return Unauthorized();
                
                var FilterParams = new UserParams
                {
                    DateTime = patientAppointmentCreate.DateTime,
                    StaffId = patientAppointmentCreate.StaffId,
                    PatientId = patientAppointmentCreate.PatientId
                };

                var Patients = await _patientRepo.GetPatients(FilterParams);
                var Staffs = await _patientRepo.GetStaffs(FilterParams);
                var Appointments = await _patientRepo.GetAppointments(FilterParams);
                
                //Cheking existence of patient and staff
                if(Patients.Exists(p => p.Id == patientAppointmentCreate.PatientId) && 
                    Staffs.Exists(s => s.Id == patientAppointmentCreate.StaffId)){
                    
                    Console.WriteLine(111);

                        //Cheking existence of appointment
                    if(Appointments.Exists(
                        x => x.DateTime == patientAppointmentCreate.DateTime && x.StaffId == patientAppointmentCreate.StaffId ||
                        x.DateTime == patientAppointmentCreate.DateTime && x.PatientId == patientAppointmentCreate.PatientId)){
                        Console.WriteLine(444);
                        return BadRequest(ModelState);
                    } 
                    else{
                        Console.WriteLine(333);
                        var appointment = _patientMapper.Map<Appointment>(patientAppointmentCreate);
                        _patientRepo.Add(appointment); 

                        if (await _patientRepo.SaveAll())
                            return StatusCode(201); 

                        throw new Exception("Ошибка при сохранении appointment");
                    }
                }
                else{
                    Console.WriteLine(555);
                     return BadRequest(ModelState);
                }                    
            }
            Console.WriteLine(222);
            return BadRequest(ModelState);
        }

        // GET Departments
        [HttpGet]
        public async Task<IActionResult> Departments()
        {
            var departments = await _patientRepo.GetDepartments();
            var departmentsToReturn = _patientMapper.Map<IEnumerable<SharedDepartmentsList>>(departments);

            return Ok(departmentsToReturn);
        }

        // GET Positions
        [HttpGet]
        public async Task<IActionResult> Positions()
        {
            var positions = await _patientRepo.GetPositions();
            var positionsToReturn = _patientMapper.Map<IEnumerable<SharedPositionsList>>(positions);

            return Ok(positionsToReturn);
        }
    }
}