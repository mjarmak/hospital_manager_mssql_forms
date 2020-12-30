using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hospital_manager_data_access.Entities
{
    [Table(name: "RoomData")]
    public class RoomData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long HospitalId { get; set; }

        public string Name { get; set; }

        public List<SpecialityToRoomData> Specialities { get; set; }

    }
}
