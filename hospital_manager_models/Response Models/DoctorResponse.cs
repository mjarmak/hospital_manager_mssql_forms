using System.Collections.Generic;

namespace hospital_manager_models.Models
{
    public class DoctorResponse
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public List<SpecialityResponse> Specialities { get; set; }
        
        public List<ConsultationResponse> Consultations { get; set; }
    }
}
