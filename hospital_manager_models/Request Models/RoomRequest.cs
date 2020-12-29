using System.Collections.Generic;

namespace hospital_manager_models.Models
{
    public class RoomRequest
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long HospitalId { get; set; }

        public List<long> SpecialityIds { get; set; }
    }
}
