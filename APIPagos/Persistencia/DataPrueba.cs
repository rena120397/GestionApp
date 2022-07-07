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
        public static async Task InsertData(PagosOnlineContext context, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario { NombreCompleto = "Vaxi Drez", UserName="vaxidrez", Email="vaxi.drez@gmail.com"};
                await usuarioManager.CreateAsync(usuario, "Password123$");
            }
        }
    }
}
