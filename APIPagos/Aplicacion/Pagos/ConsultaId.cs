using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
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
    public class ConsultaId
    {
        public class PagoUnico : IRequest<Pago>
        {
            public int Id_pago { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<PagoUnico>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Id_pago).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<PagoUnico, Pago>
        {
            private readonly PagosOnlineContext _context;
            private readonly IUsuarioSesion _usuarioSesion;

            public Manejador(PagosOnlineContext context, IUsuarioSesion usuarioSesion)
            {
                this._context = context;
                _usuarioSesion = usuarioSesion;
            }

            public async Task<Pago> Handle(PagoUnico request, CancellationToken cancellationToken)
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
                    //throw new Exception("No se pudo eliminar la categoria");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el pago" });
                }

                return pago;
            }
        }
    }
}
