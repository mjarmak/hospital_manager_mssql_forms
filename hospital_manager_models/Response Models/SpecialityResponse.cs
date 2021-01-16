using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_manager_models.Models
{
    [Table(name: "SpecialityResponse")]
    public class SpecialityResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
