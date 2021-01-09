using hospital_manager_models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_manager_data_access.Entities
{
    [Table(name: "DoctorResponse")]
    public class DoctorResponse
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public List<SpecialityResponse> Specialities { get; set; }
        
        public List<ConsultationResponse> Consultations { get; set; }


    }
}
