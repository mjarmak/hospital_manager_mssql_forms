using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace hospital_manager_data_access.Entities
{
    [Table(name: "Hospital")]
    public class HospitalData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public AddressData Address { get; set; }
        public List<OpeningHoursData> OpeningHours { get; set; }
        public List<RoomData> Rooms { get; set; }
    }

    [Table(name: "Address")]
    public class AddressData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string BoxNumber { get; set; }
    }

    [Table(name: "WorkHours")]
    public class OpeningHoursData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Day { get; set; }
        public int HourFrom { get; set; }
        public int HourTo { get; set; }
        public int MinuteTo { get; set; }
        public int MinuteFrom { get; set; }
        public bool Closed { get; set; }
    }
}
