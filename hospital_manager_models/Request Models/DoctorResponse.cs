using hospital_manager_models.Models;
using System.Collections.Generic;

namespace hospital_manager_data_access.Entities
{
    public class DoctorRequest
    {
        public string Username { get; set; }

        public List<SpecialityRequest> Specialities { get; set; }
        
        public List<ConsultationRequest> Consultations { get; set; }


    }
}
