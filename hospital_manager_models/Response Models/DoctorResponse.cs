using hospital_manager_models.Models;
using System.Collections.Generic;

namespace hospital_manager_data_access.Entities
{
    public class Doctor
    {
        public string Username { get; set; }
        
        public List<SpecialityResponse> Specialities { get; set; }
        
        public List<ConsultationResponse> Consultations { get; set; }


    }
}
