using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio
{
    public class Categoria
    {
        [Key]
        public int Id_categoria { get; set; }

        public string nombre_categoria { get; set; }
        public Pago pago { get; set; }
    }
}
