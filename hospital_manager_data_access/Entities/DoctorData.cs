using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hospital_manager_data_access.Entities
{
    [Table(name: "Doctor")]
    public class DoctorData
    {
        [Key]
        public string Username { get; set; }

        public List<SpecialityToDoctorData> Specialities { get; set; }

        public List<ConsultationData> Consultations { get; set; }


    }
}
