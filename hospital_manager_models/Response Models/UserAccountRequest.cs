using System.ComponentModel.DataAnnotations.Schema;
using hospital_manager_data_access.Entities;

namespace hospital_manager_models.Models
{
    [Table(name: "UserAccountRequest")]
    public class UserAccountRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string Password { get; set; }
        public DoctorRequest DoctorRequest { get; set; }
        public enum UserGenderEnum
        {
            MALE,
            FEMALE,
            UNKNOWN
        }
    }
}
