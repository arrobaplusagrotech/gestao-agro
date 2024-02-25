namespace AgroTech.DataAccessLayer.Models
{
    public class Animal(int farmId, decimal startWeight, decimal currentWeight, string sex,
        bool status, int? pastureId = null, int? phaseId = null, DateTime? lastChangePhase = null)
    {
        public int FarmId { get; set; } = farmId;
        public decimal StartWeight { get; set; } = startWeight;
        public decimal CurrentWeight { get; set; } = currentWeight;
        public string Sex { get; set; } = sex;
        public bool Status { get; set; } = status;
        public int? PastureId { get; set; } = pastureId;
        public int? PhaseId { get; set; } = phaseId;
        public DateTime? LastChangePhase { get; set; } = lastChangePhase;
        public int DaysInPhase
        {
            get
            {
                if (LastChangePhase is not null && PhaseId is not null)
                {
                 
                    TimeSpan difference = DateTime.Now.Subtract(LastChangePhase.Value);
                    return difference.Days;
                }
                return 0;
            }
        }
    }
}
