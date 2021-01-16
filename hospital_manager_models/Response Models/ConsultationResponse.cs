using hospital_manager_models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hospital_manager_models.Models
{
    public class ConsultationResponse
    {
        public long Id { get; set; }

        public HospitalResponse Hospital { get; set; }

        public SpecialityResponse Speciality { get; set; }

        public int Duration { get; set; }
    }
}
