using Aplicacion.Documentos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace APIPagos.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocumentosController : MyControllerBase
    {
        //http://localhost:47113/api/Categorias
        [HttpGet]
        public async Task<ActionResult<List<Documento>>> Listar()
        {
            return await mediator.Send(new Consulta.ListaDocumento());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Documento>> DetalleDocumento(int id)
        {
            return await mediator.Send(new ConsultaId.DocumentoUnico { id_documento = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(int id, Editar.Ejecuta data)
        {
            data.id_documento = id;
            return await mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(int id)
        {
            return await mediator.Send(new Eliminar.Ejecuta { id_documento = id });
        }
    }
}
