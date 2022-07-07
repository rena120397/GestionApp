using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Aplicacion.Contratos;

namespace Aplicacion.Pagos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            //public Guid Id_usuario { get; set; }
            public int? id_categoria { get; set; }
            public string nombre_pago { get; set; }
            public string fechaingreso { get; set; }
            public string fechapago { get; set; }
            public int? alerta { get; set; }
            public int? estado { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                //RuleFor(x => x.Id_usuario).NotEmpty();
                RuleFor(x => x.id_categoria).NotEmpty();
                RuleFor(x => x.nombre_pago).NotEmpty();
                RuleFor(x => x.fechaingreso).NotEmpty();
                RuleFor(x => x.fechapago).NotEmpty();
                RuleFor(x => x.alerta).NotEmpty();
                RuleFor(x => x.estado).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly PagosOnlineContext _context;
            private readonly IUsuarioSesion _usuarioSesion;

            public Manejador(IUsuarioSesion usuarioSesion, PagosOnlineContext context)
            {
                _context = context;
                _usuarioSesion = usuarioSesion;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var idUsuario = _usuarioSesion.ObtenerUsuarioSesion();

                if(idUsuario==null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el usuario" });
                }

                Guid usuario = new Guid(idUsuario);

                var pago = new Pago
                {
                    Id_usuario = usuario,
                    id_categoria = request.id_categoria,
                    nombre_pago = request.nombre_pago,
                    fechaingreso = request.fechaingreso,
                    fechapago = request.fechapago,
                    alerta = request.alerta,
                    estado = request.estado
                };

                _context.Pago.Add(pago);

                var valor = await _context.SaveChangesAsync();

                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.NotModified, new { mensaje = "No se insertó el pago" });
            }
        }
    }
}
