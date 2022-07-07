using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPagos.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class ValidadorController : ControllerBase
    {
        [HttpGet]
        public ActionResult<object> validar()
        {
            var mensaje = new { mensaje = "Funcionando correctamente!" };
            return mensaje;
        }
    }
}
