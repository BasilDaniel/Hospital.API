using System;
using System.ComponentModel.DataAnnotations;
using Hospital.API.Models;

namespace Hospital.API.Dtos
{
    public class StaffRegister
    {
        [Required (ErrorMessage ="Укажите имя")]
        public string Name { get; set; }

        [Required (ErrorMessage ="Укажите пароль")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Пароль должен содержать боллее 4 знаков")]
        public string Password { get; set; }

        [Required (ErrorMessage ="Укажите фамилию")]
        public string FamilyName { get; set; }

        [Required (ErrorMessage ="Укажите отчество")]
        public string MiddleName { get; set; }
        
        // [Required]
        public DateTime Birthdate { get; set; }

        [Required (ErrorMessage ="Укажите должность")]
        public Position Position { get; set; }

        [Required (ErrorMessage ="Укажите отделение")]
        public Department Department { get; set; }
    }
}