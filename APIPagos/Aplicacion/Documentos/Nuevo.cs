using Aplicacion.Contratos;
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

namespace Aplicacion.Documentos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            //public Guid id_usuario { get; set; }
            public string nombre_documento { get; set; }
            public string documento { get; set; }
            public string fechacreacion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                //RuleFor(x => x.id_usuario).NotEmpty();
                RuleFor(x => x.nombre_documento).NotEmpty();
                RuleFor(x => x.documento).NotEmpty();
                RuleFor(x => x.fechacreacion).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly PagosOnlineContext _context;
            private readonly IUsuarioSesion _usuarioSesion;

            public Manejador(IUsuarioSesion usuarioSesion,PagosOnlineContext context)
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

                var documento = new Documento
                {
                    id_usuario = new Guid(idUsuario),
                    nombre_documento = request.nombre_documento,
                    documento = request.documento,
                    fechacreacion = request.fechacreacion
                };

                _context.Documento.Add(documento);

                //try
                //{
                    var valor = await _context.SaveChangesAsync();
                //catch(Exception ex)
                //{
                //    throw new Exception(ex.Message);
                //}
                
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.NotModified, new { mensaje = "No se insertó el documento" });
            }
        }
    }
}
