namespace hospital_manager_models.Models
{
    public class AppointmentResponse
    {
        public long Id { get; set; }

        public string PatientUsername { get; set; }

        public string DoctorUsername { get; set; }

        public long RoomId { get; set; }

        public long HospitalId { get; set; }

        public int Duration { get; set; }

        public string Description { get; set; }

        public string From { get; set; }

        public string To { get; set; }
    }
}
