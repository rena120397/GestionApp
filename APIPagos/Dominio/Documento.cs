using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio
{
    public class Documento
    {
        [Key]
        public int? id_documento { get; set; }  
        public Guid id_usuario { get; set; }
        public string nombre_documento { get; set; }
        public string documento { get; set; }
        public string fechacreacion { get; set; }
    }
}
