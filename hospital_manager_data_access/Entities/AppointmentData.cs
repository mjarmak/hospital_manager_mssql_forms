using hospital_manager_models.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_manager_data_access.Entities
{
    [Table(name: "Appointment")]
    public class AppointmentData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string PatientUsername { get; set; }

        public string DoctorUsername { get; set; }

        public long RoomId { get; set; }

        public long HospitalId { get; set; }

        public int Duration { get; set; }

        public string Description { get; set; }

        public string From { get; set; }

        public string To { get; set; }
    }

}
