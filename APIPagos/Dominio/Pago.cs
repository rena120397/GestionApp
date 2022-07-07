using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio
{
    public class Pago
    {
        [Key]
        public int? Id_pago {get;set;}
        public Guid Id_usuario { get;set;}
        public int? id_categoria { get;set;}
        public string nombre_pago { get;set;}
        public string fechaingreso { get;set;}
        public string fechapago { get;set;}
        public int? alerta { get; set; }
        public int? estado { get; set; }
        public Categoria Categoria { get; set; }
    }
}
