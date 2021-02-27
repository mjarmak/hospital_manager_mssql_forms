using System;

namespace hospital_manager_models.Models
{
    public class AppointmentRequest
    {
        public long Id { get; set; }

        public string PatientUsername { get; set; }

        public string DoctorUsername { get; set; }

        public long RoomId { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        
    }




}
