using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hospital_manager_models.Models
{
    class SpecialityToDoctor
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
