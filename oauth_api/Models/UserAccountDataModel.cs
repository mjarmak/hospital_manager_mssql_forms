using System.ComponentModel.DataAnnotations;

namespace authentication_api
{
    public class UserAccountDataModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage = "Birthdate is required is required")]
        public string BirthDate { get; set; }
        public bool Profession { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
