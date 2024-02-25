using AgroTech.DataAccessLayer.Models;

namespace AgroTech.BusinessLogicLayer.Contracts
{
    public interface IControlPanelBLL
    {
        Task<GlobalFarmDataReporter> Get();
    }
}
