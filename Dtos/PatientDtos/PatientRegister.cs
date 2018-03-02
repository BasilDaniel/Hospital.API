using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.API.Dtos
{
    public class PatientRegister
    {
        [Required (ErrorMessage ="Укажите логин")]
        public string Login { get; set; }

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
    }
}