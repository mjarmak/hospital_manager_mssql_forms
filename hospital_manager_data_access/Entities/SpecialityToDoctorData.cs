using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_manager_data_access.Entities
{
    [Table(name: "SpecialityToDoctor")]
    public class SpecialityToDoctorData
    {
        public long Id { get; set; }

        public string DoctorUsername { get; set; }

        public SpecialityToDoctorData(string doctorUsername, long id)
        {
            DoctorUsername = doctorUsername;
            Id = id;
        }
    }
}
