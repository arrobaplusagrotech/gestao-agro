using AgroTech.Client.Services.Contracts;
using AgroTech.DataAccessLayer.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace AgroTech.Client.Services
{
    public class ControlPanelService : IControlPanelService
    {
        private readonly HttpClient _httpClient;
        public ControlPanelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> Get()
        {
            var httpRespose = _httpClient.GetAsync("api/v1/global-farms");
            return await httpRespose;
        }
    }
}
