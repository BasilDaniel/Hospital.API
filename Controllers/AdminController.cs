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
    [Authorize(Roles = "admin")]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly IDatingRepository _adminRepo;
        private readonly IMapper _adminMapper;
        public AdminController(IDatingRepository repo, IMapper mapper)
        {
            _adminMapper = mapper;
            _adminRepo = repo;
        }

        //------------------------Patients------------------------

        // GET Patients
        [HttpGet]
        public async Task<IActionResult> Patients(UserParams userParams)
        {
            var patients = await _adminRepo.GetPatients(userParams);
            var patientToReturn = _adminMapper.Map<IEnumerable<SharedPatientsList>>(patients);
            Response.AddPagination(patients.CurrentPage, patients.PageSize, patients.TotalCount, patients.TotalPages);

            return Ok(patientToReturn);
        }

        // GET Patient/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Patient(int id)
        {
            var patient = await _adminRepo.GetPatient(id);
            var patientToReturn = _adminMapper.Map<SharedPatientDetailed>(patient);

            return Ok(patientToReturn);
        }

        // PUT Patient/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] AdminPatientUpdate adminPatientUpdate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var patient = await _adminRepo.GetPatient(id);

            if (patient == null)
                return NotFound($"Пользователя с Id {id} не существует");


            _adminMapper.Map(adminPatientUpdate, patient);

            if(await _adminRepo.SaveAll())
                return NoContent();
                
            throw new Exception($"Пользователь {id} НЕ обновлен!");
        }

        //------------------------Staffs------------------------

        // GET Staffs
        [HttpGet]
        public async Task<IActionResult> Staffs(UserParams userParams)
        {
            var staffs = await _adminRepo.GetStaffs(userParams);
            var staffsToReturn = _adminMapper.Map<IEnumerable<SharedStaffsList>>(staffs);
            Response.AddPagination(staffs.CurrentPage, staffs.PageSize, staffs.TotalCount, staffs.TotalPages);

            return Ok(staffsToReturn);
        }

        // GET Staff/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Staff(int id)
        {
            var staff = await _adminRepo.GetStaff(id);
            var staffToReturn = _adminMapper.Map<SharedStaffDetailed>(staff);

            return Ok(staffToReturn);
        }

        // PUT Staff/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] AdminStaffUpdate adminStaffUpdate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var staff = await _adminRepo.GetStaff(id);

            if (staff == null)
                return NotFound($"Пользователя с Id {id} не существует");


            _adminMapper.Map(adminStaffUpdate, staff);

            if(await _adminRepo.SaveAll())
                return NoContent();
                
            throw new Exception($"Пользователь {id} НЕ обновлен!");
        }

        // DELETE Staff/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var staff = await _adminRepo.GetStaff(id);

            return Ok(staff);
        }

        //------------------------Admins------------------------

        // GET Admins
        [HttpGet]
        public async Task<IActionResult> Admins()
        {
            var admins = await _adminRepo.GetAdmins();
            var adminsToReturn = _adminMapper.Map<IEnumerable<AdminsList>>(admins);

            return Ok(adminsToReturn);
        }

        // GET Admin/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Admin(int id)
        {
            var admin = await _adminRepo.GetAdmin(id);
            var adminToReturn = _adminMapper.Map<AdminDetailed>(admin);

            return Ok(adminToReturn);
        }

        // PUT Admin/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] AdminUpdate adminUpdate)
        {
             if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var admin = await _adminRepo.GetAdmin(id);

            if (admin == null)
                return NotFound($"Пользователя с Id {id} не существует");


            _adminMapper.Map(adminUpdate, admin);

            if(await _adminRepo.SaveAll())
                return NoContent();
                
            throw new Exception($"Пользователь {id} НЕ обновлен!");
        }

        // DELETE Admin/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _adminRepo.GetAdmin(id);

            return Ok(admin);
        }

        //------------------------Appointments------------------------

        // GET Appointments
        [HttpGet]
        public async Task<IActionResult> Appointments(UserParams userParams)
        {
            var appointments = await _adminRepo.GetAppointments(userParams);
            var appointmentsToReturn = _adminMapper.Map<IEnumerable<SharedAppointmentsList>>(appointments);
            Response.AddPagination(appointments.CurrentPage, appointments.PageSize, appointments.TotalCount, appointments.TotalPages);
            
            return Ok(appointmentsToReturn);
        }

        // GET Appointment/id
        [HttpGet("{id}", Name = "GetAppointment")]
        public async Task<IActionResult> Appointment(int id)
        {
            var appointment = await _adminRepo.GetAppointment(id);
            var appointmentToReturn = _adminMapper.Map<SharedAppointmentDetailed>(appointment);

            return Ok(appointmentToReturn);
        }

        // PUT Appointment/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AdminAppointmentUpdate adminAppointmentUpdate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var appointment = await _adminRepo.GetAppointment(id);

            if (appointment == null)
                return NotFound($"События с Id {id} не существует");


            _adminMapper.Map(adminAppointmentUpdate, appointment);

            if(await _adminRepo.SaveAll())
                return NoContent();
                
            throw new Exception($"Событие {id} НЕ обновлено!");
        }

        // DELETE Appointment/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _adminRepo.GetAppointment(id);
            if(appointment != null)
             _adminRepo.Delete(appointment);
            
            if (await _adminRepo.SaveAll())
                return NoContent();
            
            throw new Exception("Ошибка при удалении Appointment");
        }

        // POST Appointment
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] AdminAppointmentCreate adminAppointmentCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(adminAppointmentCreate.DateTime != null && adminAppointmentCreate.DateTime > DateTime.Now){
                
                var FilterParams = new UserParams
                {
                    DateTime = adminAppointmentCreate.DateTime,
                    StaffId = adminAppointmentCreate.StaffId,
                    PatientId = adminAppointmentCreate.PatientId
                };

                var Patients = await _adminRepo.GetPatients(FilterParams);
                var Staffs = await _adminRepo.GetStaffs(FilterParams);
                var Appointments = await _adminRepo.GetAppointments(FilterParams);
                
                //Cheking existence of patient and staff
                if(Patients.Exists(p => p.Id == adminAppointmentCreate.PatientId) && 
                    Staffs.Exists(s => s.Id == adminAppointmentCreate.StaffId)){
                    
                    Console.WriteLine(111);

                        //Cheking existence of appointment
                    if(Appointments.Exists(
                        x => x.DateTime == adminAppointmentCreate.DateTime && x.StaffId == adminAppointmentCreate.StaffId ||
                        x.DateTime == adminAppointmentCreate.DateTime && x.PatientId == adminAppointmentCreate.PatientId)){
                        Console.WriteLine(444);
                        return BadRequest(ModelState);
                    } 
                    else{
                        Console.WriteLine(333);
                        var appointment = _adminMapper.Map<Appointment>(adminAppointmentCreate);
                        _adminRepo.Add(appointment); 

                        if (await _adminRepo.SaveAll())
                            return StatusCode(201); 

                        throw new Exception("Ошибка при сохранении appointment");
                    }
                }
                else{
                     return BadRequest(ModelState);
                }                    
            }
            Console.WriteLine(222);
            return BadRequest(ModelState);
        }

        //------------------------Departments------------------------

        // GET Departments
        [HttpGet]
        public async Task<IActionResult> Departments()
        {
            var departments = await _adminRepo.GetDepartments();
            var departmentsToReturn = _adminMapper.Map<IEnumerable<SharedDepartmentsList>>(departments);

            return Ok(departmentsToReturn);
        }

        // GET Department/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Department(int id)
        {
            var department = await _adminRepo.GetDepartment(id);
            var departmentToReturn = _adminMapper.Map<SharedDepartmentDetailed>(department);

            return Ok(departmentToReturn);
        }

        // PUT Department/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] AdminDepartmentUpdate adminDepartmentUpdate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = await _adminRepo.GetDepartment(id);

            if (department == null)
                return NotFound($"Отделения с Id {id} не существует");


            _adminMapper.Map(adminDepartmentUpdate, department);

            if(await _adminRepo.SaveAll())
                return NoContent();
                
            throw new Exception($"Отделение {id} НЕ обновлено!");
        }

        // DELETE Department/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _adminRepo.GetDepartment(id);
            if(department != null)
             _adminRepo.Delete(department);
            
            if (await _adminRepo.SaveAll())
                return NoContent();
            
            throw new Exception("Ошибка при удалении Department");
        }

        // POST Department
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] AdminDepartmentCreate adminDepartmentCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(adminDepartmentCreate.Name != null){

                var Departments = await _adminRepo.GetDepartments();
                
                //Cheking existence of Department
                if(Departments.Any(p => p.Name == adminDepartmentCreate.Name))
                {
                    Console.WriteLine(111);
                    return BadRequest(ModelState);
                }
                else
                {
                    Console.WriteLine(333);
                    var department = _adminMapper.Map<Department>(adminDepartmentCreate);
                    _adminRepo.Add(department); 

                    if (await _adminRepo.SaveAll())
                        return StatusCode(201); 

                    throw new Exception("Ошибка при сохранении appointment");
                }
            }
            else{
                 return BadRequest(ModelState);
            }                    
        }

        //------------------------Positions------------------------

        // GET Positions
        [HttpGet]
        public async Task<IActionResult> Positions()
        {
            var positions = await _adminRepo.GetPositions();
            var positionsToReturn = _adminMapper.Map<IEnumerable<SharedPositionsList>>(positions);

            return Ok(positionsToReturn);
        }

        // GET Position/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Position(int id)
        {
            var position = await _adminRepo.GetPosition(id);
            var positionToReturn = _adminMapper.Map<SharedPositionDetailed>(position);

            return Ok(positionToReturn);
        }

        // PUT Position/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosition(int id, [FromBody] AdminPositionUpdate adminPositionUpdate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var position = await _adminRepo.GetPosition(id);

            if (position == null)
                return NotFound($"Позиции с Id {id} не существует");


            _adminMapper.Map(adminPositionUpdate, position);

            if(await _adminRepo.SaveAll())
                return NoContent();
                
            throw new Exception($"Позиция {id} НЕ обновлена!");
        }

        // DELETE Position/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            var position = await _adminRepo.GetPosition(id);
            if(position != null)
             _adminRepo.Delete(position);
            
            if (await _adminRepo.SaveAll())
                return NoContent();
            
            throw new Exception("Ошибка при удалении Position");
        }

        // POST Position
        [HttpPost]
        public async Task<IActionResult> CreatePosition([FromBody] AdminPositionCreate adminPositionCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(adminPositionCreate.Name != null){

                var Positions = await _adminRepo.GetPositions();
                
                //Cheking existence of Department
                if(Positions.Any(p => p.Name == adminPositionCreate.Name))
                {
                    Console.WriteLine(111);
                    return BadRequest(ModelState);
                }
                else
                {
                    Console.WriteLine(333);
                    var position = _adminMapper.Map<Position>(adminPositionCreate);
                    _adminRepo.Add(position); 

                    if (await _adminRepo.SaveAll())
                        return StatusCode(201); 

                    throw new Exception("Ошибка при сохранении position");
                }
            }
            else{
                 return BadRequest(ModelState);
            }
        }
        
        //------------------------Diseases------------------------

        // GET Diseases
        [HttpGet]
        public async Task<IActionResult> Diseases(UserParams userParams)
        {
            var diseases = await _adminRepo.GetDiseases(userParams);
            var diseasesToReturn = _adminMapper.Map<IEnumerable<SharedDiseasesList>>(diseases);
            Response.AddPagination(diseases.CurrentPage, diseases.PageSize, diseases.TotalCount, diseases.TotalPages);
            
            return Ok(diseasesToReturn);
        }

        // GET Disease/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Disease(int id)
        {
            var disease = await _adminRepo.GetDisease(id);
            var diseaseToReturn = _adminMapper.Map<SharedDiseaseDetailed>(disease);

            return Ok(diseaseToReturn);
        }

        // PUT Disease/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDisease(int id, [FromBody] AdminDiseaseUpdate adminDiseaseUpdate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var disease = await _adminRepo.GetDisease(id);

            if (disease == null)
                return NotFound($"Заболевание с Id {id} не существует");


            _adminMapper.Map(adminDiseaseUpdate, disease);

            if(await _adminRepo.SaveAll())
                return NoContent();
                
            throw new Exception($"Заболевание {id} НЕ обновлено!");
        }

        // DELETE Disease/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            var disease = await _adminRepo.GetDisease(id);
            if(disease != null)
             _adminRepo.Delete(disease);
            
            if (await _adminRepo.SaveAll())
                return NoContent();
            
            throw new Exception("Ошибка при удалении Disease");
        }

        // POST Disease
        [HttpPost]
        public async Task<IActionResult> CreateDisease([FromBody] AdminDiseaseCreate adminDiseaseCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(adminDiseaseCreate.Name != null){
                 var FilterParams = new UserParams
                {
                    Name = adminDiseaseCreate.Name,
                };

                var Diseases = await _adminRepo.GetDiseases(FilterParams);
                
                //Cheking existence of Disease
                if(Diseases.Any(p => p.Name == adminDiseaseCreate.Name))
                {
                    Console.WriteLine(111);
                    return BadRequest(ModelState);
                }
                else
                {
                    Console.WriteLine(333);
                    var disease = _adminMapper.Map<Disease>(adminDiseaseCreate);
                    _adminRepo.Add(disease); 

                    if (await _adminRepo.SaveAll())
                        return StatusCode(201); 

                    throw new Exception("Ошибка при сохранении position");
                }
            }
            else{
                 return BadRequest(ModelState);
            }
        }

        //------------------------PatientDiseases------------------------

        // GET PatientDiseases
        [HttpGet]
        public async Task<IActionResult> PatientDiseases(UserParams userParams)
        {
            var patientDiseases = await _adminRepo.GetPatientDiseases(userParams);
            var patientDiseasesToReturn = _adminMapper.Map<IEnumerable<SharedPatientDiseasesDetailedList>>(patientDiseases);
            Response.AddPagination(patientDiseases.CurrentPage, patientDiseases.PageSize, patientDiseases.TotalCount, patientDiseases.TotalPages);
            
            return Ok(patientDiseasesToReturn);
        }

        // PUT PatientDiseases/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatientDisease(int id, [FromBody] SharedPatientDiseaseUpdate sharedPatientDiseaseUpdate)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var patientDisease = await _adminRepo.GetPatientDisease(id);

            if (patientDisease == null)
                return NotFound($"Элемента с Id {id} не существует");


            _adminMapper.Map(sharedPatientDiseaseUpdate, patientDisease);

            if(await _adminRepo.SaveAll())
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

                var patientDisease = _adminMapper.Map<PatientDisease>(sharedPatientDiseaseCreate);
                _adminRepo.Add(patientDisease); 

                if (await _adminRepo.SaveAll())
                    return StatusCode(201); 

                throw new Exception("Ошибка при сохранении position");
            }
            else{
                 return BadRequest(ModelState);
            }
        }
    }
}