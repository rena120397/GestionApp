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

namespace Aplicacion.Categorias
{
    public class Consulta
    {
        public class ListaCategoria : IRequest<List<Categoria>>
        {
        }

        public class Manejador : IRequestHandler<ListaCategoria, List<Categoria>>
        {
            private readonly PagosOnlineContext _context;

            public Manejador(PagosOnlineContext context)
            {
                _context = context;
            } 

            public async Task<List<Categoria>> Handle(ListaCategoria request, CancellationToken cancellationToken)
            {
                var categorias = await _context.Categoria.ToListAsync();
                return categorias;
            }
        }
    }
}
