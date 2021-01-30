using System.Collections.Generic;

namespace hospital_manager_models.Models
{
    public class HospitalRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public AddressRequest Address { get; set; }
        public List<OpeningHoursRequest> OpeningHours { get; set; }
        public List<RoomRequest> Rooms { get; set; }
    }

    public class AddressRequest
    {
        public long Id { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string BoxNumber { get; set; }
    }

    public class OpeningHoursRequest
    {
        public long Id { get; set; }
        public string Day { get; set; }
        public int HourFrom { get; set; }
        public int HourTo { get; set; }
        public int MinuteTo { get; set; }
        public int MinuteFrom { get; set; }
        public bool Closed { get; set; }

        public OpeningHoursRequest()
        {
        }
        public OpeningHoursRequest(long Id, string Day, int HourFrom, int HourTo, int MinuteFrom, int MinuteTo, bool Closed)
        {
            this.Id = Id;
            this.Day = Day;
            this.HourFrom = HourFrom;
            this.HourTo = HourTo;
            this.MinuteFrom = MinuteFrom;
            this.MinuteTo = MinuteTo;
            this.Closed = Closed;
        }

        public static List<OpeningHoursRequest> GetDays()
        {
            return new List<OpeningHoursRequest>
            {
                new OpeningHoursRequest(0, "MONDAY", 0, 0, 0, 0, false),
                new OpeningHoursRequest(0, "TUESDAY", 0, 0, 0, 0, false),
                new OpeningHoursRequest(0, "WEDNESDAY", 0, 0, 0, 0, false),
                new OpeningHoursRequest(0, "THURSDAY", 0, 0, 0, 0, false),
                new OpeningHoursRequest(0, "FRIDAY", 0, 0, 0, 0, false),
                new OpeningHoursRequest(0, "SATURDAY", 0, 0, 0, 0, false),
                new OpeningHoursRequest(0, "SUNDAY", 0, 0, 0, 0, false)
            };
        }
    }
}
