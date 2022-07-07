using Aplicacion.Categorias;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIPagos.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriasController : MyControllerBase
    {
        //http://localhost:47113/api/Categorias
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Listar()
        {
            return await mediator.Send(new Consulta.ListaCategoria());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> DetalleCategoria(int id)
        {
            return await mediator.Send(new ConsultaId.CategoriaUnica { Id_categoria = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(int id, Editar.Ejecuta data)
        {
            data.Id_categoria = id;
            return await mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(int id)
        {
            return await mediator.Send(new Eliminar.Ejecuta { Id_categoria=id });
        }

    }
}
