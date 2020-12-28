namespace authentication_api
{
    public class UserAccountDataModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public bool Profession { get; set; }
        public string Password { get; set; }
    }

}
