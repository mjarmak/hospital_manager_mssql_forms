using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_manager_data_access.Entities
{
    [Table(name: "SpecialityToRoom")]
    public class SpecialityToRoomData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long SpecialityId { get; set; }
        
    }
}
