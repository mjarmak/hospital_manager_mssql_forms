using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hospital_manager_data_access.Entities
{
    [Table(name: "Speciality")]
    public class SpecialityData
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
