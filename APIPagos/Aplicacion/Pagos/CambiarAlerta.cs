using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Pagos
{
    public class CambiarAlerta
    {
        public class Ejecuta : IRequest
        {
            public int Id_pago { get; set; }

            public int? alerta { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {          
                RuleFor(x => x.alerta).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly PagosOnlineContext _context;
            private readonly IUsuarioSesion _usuarioSesion;

            public Manejador(PagosOnlineContext context, IUsuarioSesion usuarioSesion)
            {
                _context = context;
                _usuarioSesion = usuarioSesion;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var idUsuario = _usuarioSesion.ObtenerUsuarioSesion();

                if (idUsuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el usuario" });
                }

                Guid usuario = new Guid(idUsuario);

                var pago = await _context.Pago.Where(x => x.Id_pago == request.Id_pago && x.Id_usuario == usuario).FirstOrDefaultAsync();

                if (pago == null)
                {
                    //throw new Exception("La Categoria no existe");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el pago" });
                }

                pago.alerta = request.alerta;

                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.NotModified, new { mensaje = "No se cambió el estado de alerta del pago" });
            }
        }
    }
}
