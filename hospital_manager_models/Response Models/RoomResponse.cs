namespace hospital_manager_models.Models
{
    public class RoomResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long HospitalId { get; set; }

        public SpecialityResponse Speciality { get; set; }
    }
}
