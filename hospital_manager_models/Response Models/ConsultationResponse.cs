using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hospital_manager_data_access.Entities
{
    public class ConsultationResponse
    {
        public long Id { get; set; }

        public long HospitalId { get; set; }

        public int Duration { get; set; }
    }
}
