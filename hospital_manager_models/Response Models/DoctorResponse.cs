using hospital_manager_models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hospital_manager_data_access.Entities
{
    public class Doctor
    {
        public string Username { get; set; }

        public List<SpecialityResponse> Specialities { get; set; }
        
        public List<ConsultationResponse> Consultations { get; set; }


    }
}
