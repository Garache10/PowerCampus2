using Dominio;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertarData(PowerCampus2Context context, UserManager<T_user> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new T_user
                {
                    firstname = "Administrador",
                    lastname = "Administrador",
                    UserName = "Admin",
                    Email = "admin@mail.com"
                };
                await usuarioManager.CreateAsync(usuario, "Password1234$");
            }
        }
    }
}
