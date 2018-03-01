using System;
using System.Collections.Generic;
using System.Linq;
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

    [Authorize(Roles = "staff")]
    [Route("api/[controller]/[action]")]
    public class StaffController : Controller
    {
        private readonly IDatingRepository _staffRepo;
        private readonly IMapper _staffMapper;
        public StaffController(IDatingRepository repo, IMapper mapper)
        {
            _staffMapper = mapper;
            _staffRepo = repo;
        }

        // GET Patients
        [HttpGet]
        public async Task<IActionResult> Patients(UserParams userParams)
        {
            var patients = await _staffRepo.GetPatients(userParams);
            var patientToReturn = _staffMapper.Map<IEnumerable<SharedPatientsList>>(patients);
            Response.AddPagination(patients.CurrentPage, patients.PageSize, patients.TotalCount, patients.TotalPages);


            return Ok(patientToReturn);
        }
        
        // GET Patient/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Patient(int id)
        {
            var patient = await _staffRepo.GetPatient(id);
            var patientToReturn = _staffMapper.Map<SharedPatientDetailed>(patient);

            return Ok(patientToReturn);
        }

        // PUT Patient/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] StaffPatientUpdate staffPatientUpdate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var patient = await _staffRepo.GetPatient(id);

            if (patient == null)
                return NotFound($"Пользователя с Id {id} не существует");


            _staffMapper.Map(staffPatientUpdate, patient);

            if(await _staffRepo.SaveAll())
                return NoContent();
                
            throw new Exception($"Пользователь {id} НЕ обновлен!");
        }

        // GET Staffs
        [HttpGet]
        public async Task<IActionResult> Staffs(UserParams userParams)
        {
            var staffs = await _staffRepo.GetStaffs(userParams);
            var staffsToReturn = _staffMapper.Map<IEnumerable<SharedStaffsList>>(staffs);
            Response.AddPagination(staffs.CurrentPage, staffs.PageSize, staffs.TotalCount, staffs.TotalPages);
            
            return Ok(staffsToReturn);
        }

        // GET Staff/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Staff(int id)
        {
            var staff = await _staffRepo.GetStaff(id);
            var staffToReturn = _staffMapper.Map<SharedStaffDetailed>(staff);

            return Ok(staffToReturn);
        }

        // GET Appointment/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Appointment(int id)
        {
            var appointment = await _staffRepo.GetAppointment(id);
            var appointmentToReturn = _staffMapper.Map<SharedAppointmentDetailed>(appointment);

            return Ok(appointmentToReturn);
        }

        // GET PatientDiseases
        [HttpGet]
        public async Task<IActionResult> PatientDiseases(UserParams userParams)
        {
            var patientDiseases = await _staffRepo.GetPatientDiseases(userParams);
            var patientDiseasesToReturn = _staffMapper.Map<IEnumerable<SharedPatientDiseasesDetailedList>>(patientDiseases);
            Response.AddPagination(patientDiseases.CurrentPage, patientDiseases.PageSize, patientDiseases.TotalCount, patientDiseases.TotalPages);
            
            return Ok(patientDiseasesToReturn);
        }

        // PUT PatientDiseases/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatientDisease(int id, [FromBody] SharedPatientDiseaseUpdate sharedPatientDiseaseUpdate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var patientDisease = await _staffRepo.GetPatientDisease(id);

            if (patientDisease == null)
                return NotFound($"Элемента с Id {id} не существует");


            _staffMapper.Map(sharedPatientDiseaseUpdate, patientDisease);

            if(await _staffRepo.SaveAll())
                return NoContent();
                
            throw new Exception($"Элемент {id} НЕ обновлен!");
        }

        // POST PatientDisease
        [HttpPost]
        public async Task<IActionResult> CreatePatientDisease([FromBody] SharedPatientDiseaseCreate sharedPatientDiseaseCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(sharedPatientDiseaseCreate.PatientId != null && sharedPatientDiseaseCreate.DiseaseId != null){

                var patientDisease = _staffMapper.Map<PatientDisease>(sharedPatientDiseaseCreate);
                _staffRepo.Add(patientDisease); 

                if (await _staffRepo.SaveAll())
                    return StatusCode(201); 

                throw new Exception("Ошибка при сохранении position");
            }
            else{
                 return BadRequest(ModelState);
            }
        }

        // GET Departments
        [HttpGet]
        public async Task<IActionResult> Departments()
        {
            var departments = await _staffRepo.GetDepartments();
            var departmentsToReturn = _staffMapper.Map<IEnumerable<SharedDepartmentsList>>(departments);

            return Ok(departmentsToReturn);
        }

        // GET Positions
        [HttpGet]
        public async Task<IActionResult> Positions()
        {
            var positions = await _staffRepo.GetPositions();
            var positionsToReturn = _staffMapper.Map<IEnumerable<SharedPositionsList>>(positions);

            return Ok(positionsToReturn);
        }

        // GET Diseases
        [HttpGet]
        public async Task<IActionResult> Diseases(UserParams userParams)
        {
            var diseases = await _staffRepo.GetDiseases(userParams);
            var diseasesToReturn = _staffMapper.Map<IEnumerable<SharedDiseasesList>>(diseases);
            Response.AddPagination(diseases.CurrentPage, diseases.PageSize, diseases.TotalCount, diseases.TotalPages);
            
            return Ok(diseasesToReturn);
        }

        // GET Disease/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Disease(int id)
        {
            var disease = await _staffRepo.GetDisease(id);
            var diseaseToReturn = _staffMapper.Map<SharedDiseaseDetailed>(disease);

            return Ok(diseaseToReturn);
        }
    }
}