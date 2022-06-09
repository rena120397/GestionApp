using System.Runtime.InteropServices;
using System.Resources;
using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Aplicacion.ManejadorError;
using System;
using FluentValidation;

namespace Aplicacion.Seguridad
{
    public class Registrar
    {
        public class Ejecuta : IRequest<UsuarioData>{
            public string Nombre{get;set;}
            public string Apellidos{get;set;}
            public string Email{get;set;}
            public string Password{get;set;}
            public string Username{get;set;}
        }

        public class EjecutaValidador : AbstractValidator<Ejecuta>{
            public EjecutaValidador(){
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly Context _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador; 
            public Manejador(Context context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador){
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where( x => x.Email == request.Email).AnyAsync();
                if(existe){
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new {mensaje = "Existe un usuario registrado con este email"});
                }

                var existeUserName = await _context.Users.Where( x => x.UserName == request.Username).AnyAsync();
                if(existeUserName){
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new {mensaje = "Existe un usuario registrado con este UserName"});
                }

                var usuario = new Usuario{
                    NombreCompleto = request.Nombre + " " + request.Apellidos,
                    Email = request.Email,
                    UserName = request.Username
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);
                if(resultado.Succeeded)
                    return new UsuarioData{
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario),
                        Email = usuario.Email,
                        Username = usuario.UserName
                    };
                
                throw new Exception("No se pudo agregar al nuevo usuario");
            }
        }
    }
}