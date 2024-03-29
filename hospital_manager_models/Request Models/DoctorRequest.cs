﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_manager_models.Models
{
    public class DoctorRequest
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public List<long> SpecialityIds { get; set; }

        public List<ConsultationRequest> Consultations { get; set; }


    }
}
