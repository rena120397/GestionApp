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

namespace Aplicacion.Documentos
{
    public class ConsultaId
    {
        public class DocumentoUnico : IRequest<Documento>
        {
            public int id_documento { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<DocumentoUnico>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.id_documento).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<DocumentoUnico, Documento>
        {
            private readonly PagosOnlineContext _context;
            private readonly IUsuarioSesion _usuarioSesion;

            public Manejador(IUsuarioSesion usuarioSesion, PagosOnlineContext context)
            {
                _usuarioSesion = usuarioSesion;
                this._context = context;
            }

            public async Task<Documento> Handle(DocumentoUnico request, CancellationToken cancellationToken)
            {
                var idUsuario = _usuarioSesion.ObtenerUsuarioSesion();

                if (idUsuario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el usuario" });
                }

                Guid usuario = new Guid(idUsuario);
             
                var documento = await _context.Documento.Where(x => x.id_documento==request.id_documento && x.id_usuario== usuario).FirstOrDefaultAsync();

                if (documento == null)
                {
                    //throw new Exception("No se pudo eliminar la categoria");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el documento" });
                }

                return documento;
            }
        }
    }
}
