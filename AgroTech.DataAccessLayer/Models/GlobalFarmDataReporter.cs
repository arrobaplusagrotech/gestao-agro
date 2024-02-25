namespace AgroTech.DataAccessLayer.Models
{
    public class GlobalFarmDataReporter()
    {
        public ICollection<DataBase> Clients { get; set; } = [];
        public int TotalClients
        {
            get
            {
                return Clients.Count;
            }
        }
    }
}
