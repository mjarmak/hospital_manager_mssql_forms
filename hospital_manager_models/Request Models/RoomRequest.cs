using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_manager_models.Models
{
    [Table(name: "RoomRequest")]
    public class RoomRequest
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long HospitalId { get; set; }

        public List<long> SpecialityIds { get; set; }
        public RoomRequest()
        {
        }
        public RoomRequest(string Name, List<long> SpecialityIds)
        {
            this.Name = Name;
            this.HospitalId = HospitalId;
            this.SpecialityIds = SpecialityIds;
        }
    }
}
