using AgroTech.DataAccessLayer.Models;

namespace AgroTech.Client.Services.Contracts
{
    public interface IControlPanelService
    {
        Task<HttpResponseMessage> Get();
    }
}
