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

namespace Aplicacion.Documentos
{
    public class Consulta
    {
        public class ListaDocumento : IRequest<List<Documento>>
        {
        }

        public class Manejador : IRequestHandler<ListaDocumento, List<Documento>>
        {
            private readonly PagosOnlineContext _context;
            private readonly IUsuarioSesion _usuarioSesion;

            public Manejador(IUsuarioSesion usuarioSesion,PagosOnlineContext context)
            {
                _usuarioSesion = usuarioSesion;
                _context = context;
            }

            public async Task<List<Documento>> Handle(ListaDocumento request, CancellationToken cancellationToken)
            {
                var idUsuario = _usuarioSesion.ObtenerUsuarioSesion();

                if (idUsuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el usuario" });
                }

                Guid usuario = new Guid(idUsuario);

                var documentos = await _context.Documento.Where(x => x.id_usuario == usuario).ToListAsync();
                return documentos;
            }
        }
    }
}
