namespace hospital_manager_models.Models
{
    public class SpecialityRequest
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public SpecialityRequest()
        {
        }
        public SpecialityRequest(string Name)
        {
            this.Name = Name;
        }

    }
}
