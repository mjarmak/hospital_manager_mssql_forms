using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_manager_models.Models
{
    [Table(name: "RoomResponse")]
    public class RoomResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long HospitalId { get; set; }

        public List<SpecialityResponse> Specialities { get; set; }
    }
}
