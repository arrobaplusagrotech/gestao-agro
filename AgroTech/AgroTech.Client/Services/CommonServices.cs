using AgroTech.Client.Services.Contracts;

namespace AgroTech.Client.Services
{
    public class CommonServices
    {
        public static void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddTransient<IControlPanelService, ControlPanelService>();
        }
    }
}
