using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_manager_data_access.Entities
{
    [Table(name: "SpecialityToRoom")]
    public class SpecialityToRoomData
    {
        public long Id { get; set; }

        public long RoomId { get; set; }
    }
}
