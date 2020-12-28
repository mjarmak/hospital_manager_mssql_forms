using System;
using System.Collections.Generic;
using System.Text;

namespace hospital_manager_models.Models
{
    public class UserAccountModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string Password { get; set; }
    }
    public enum UserGenderEnum
    {
        MALE,
        FEMALE,
        UNKNOWN
    }
}
