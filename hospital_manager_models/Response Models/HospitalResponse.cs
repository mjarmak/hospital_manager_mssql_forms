using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_manager_models.Models
{
    [Table(name: "HospitalResponse")]
    public class HospitalResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public AddressResponse Address { get; set; }
        public List<OpeningHoursResponse> OpeningHours { get; set; }
        public List<RoomResponse> Rooms { get; set; }
    }

    [Table(name: "AddressResponse")]
    public class AddressResponse
    {
        public long Id { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string BoxNumber { get; set; }
    }

    [Table(name: "OpeningHoursResponse")]
    public class OpeningHoursResponse
    {
        public long Id { get; set; }
        public string Day { get; set; }
        public int HourFrom { get; set; }
        public int HourTo { get; set; }
        public int MinuteTo { get; set; }
        public int MinuteFrom { get; set; }
        public bool Closed { get; set; }
    }
}
