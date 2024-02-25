using AgroTech.BusinessLogicLayer.Services.Contracts;
using AgroTech.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroTech.BusinessLogicLayer.Services
{
    public class DefaultUserServices : IDefaultUserServices
    {
        private readonly UserManager<User> _userManager;

        public DefaultUserServices(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task InitializeDefaultUserAsync()
        {
            // Verifica se o usuário padrão já existe
            var defaultUser = await _userManager.FindByNameAsync("admin");

            if (defaultUser == null)
            {
                // Cria o usuário padrão se não existir
                var newUser = new User
                {
                   // UserName = "admin",
                    Email = "admin@admin.com", // Pode ser um e-mail válido
                    UserName = "admin"                             // Outras propriedades personalizadas, se necessário
                };

                var result = await _userManager.CreateAsync(newUser, "@Plus2468");

                if (result.Succeeded)
                {
                    // Adiciona o usuário padrão à função de administrador
                    await _userManager.AddToRoleAsync(newUser, "Admin");
                }
                else
                {
                    // Lidar com falhas na criação do usuário, se necessário
                }
            }
        }
    }
}
