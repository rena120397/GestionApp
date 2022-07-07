using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio
{
    public class Perfil
    {
        [Key]
        public Guid PerfilId { get; set; }
        public Guid UsuarioId { get; set; }
        public string NombrePerfil { get; set; }
    }
}
