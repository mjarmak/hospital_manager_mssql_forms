namespace hospital_manager_data_access.Entities
{
    public class ConsultationRequest
    {
        public long Id { get; set; }

        public long HospitalId { get; set; }

        public int Duration { get; set; }

        public ConsultationRequest()
        {
        }
        public ConsultationRequest(long HospitalId, int Duration)
        {
            this.HospitalId = HospitalId;
            this.Duration = Duration;
        }
    }
}
