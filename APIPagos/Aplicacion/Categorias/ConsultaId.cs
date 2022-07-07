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

namespace Aplicacion.Categorias
{
    public class ConsultaId
    {
        public class CategoriaUnica : IRequest<Categoria>
        {
            public int Id_categoria { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<CategoriaUnica>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Id_categoria).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<CategoriaUnica, Categoria>
        {
            private readonly PagosOnlineContext _context;

            public Manejador(PagosOnlineContext context)
            {
                this._context = context;
            }

            public async Task<Categoria> Handle(CategoriaUnica request, CancellationToken cancellationToken)
            {
                var categoria = await _context.Categoria.FindAsync(request.Id_categoria);

                if (categoria == null)
                {
                    //throw new Exception("No se pudo eliminar la categoria");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró la categoria" });
                }

                return categoria;
            }
        }
    }
}
