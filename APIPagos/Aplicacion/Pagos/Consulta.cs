using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio;
using System.Threading.Tasks;
using System.Threading;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using System.Net;
using System.Linq;

namespace Aplicacion.Pagos
{
    public class Consulta
    {
        public class ListaPago : IRequest<List<Pago>>
        {
        }

        public class Manejador : IRequestHandler<ListaPago, List<Pago>>
        {
            private readonly PagosOnlineContext _context;
            private readonly IUsuarioSesion _usuarioSesion;

            public Manejador(IUsuarioSesion usuarioSesion, PagosOnlineContext context)
            {
                _context = context;
                _usuarioSesion = usuarioSesion;
            }

            public async Task<List<Pago>> Handle(ListaPago request, CancellationToken cancellationToken)
            {
                var idUsuario = _usuarioSesion.ObtenerUsuarioSesion();

                if (idUsuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el usuario" });
                }

                Guid usuario = new Guid(idUsuario);

                var pagos = await _context.Pago.Where(x => x.Id_usuario == usuario).ToListAsync();
                return pagos;
            }
        }
    }
}
