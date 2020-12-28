namespace authentication_api
{
    public class UserUpdateModel
    {
        public string NameNew { get; set; }
        public string SurnameNew { get; set; }
        public string PhoneNew { get; set; }
        public bool ProfessionNew { get; set; }
        public string NamePrevious { get; set; }
        public string SurnamePrevious { get; set; }
        public string PhonePrevious { get; set; }
        public bool ProfessionPrevious { get; set; }
    }

}
