using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Configuration;
using MediatR;
using Aplicacion;
using FluentValidation;
using Persistencia;
using System.Collections.Generic;
using Dominio;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace TestUnitario
{
    [TestClass]
    public class UnitTest1
    {
        /*public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.Development.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }*/

        //prueba unitaria para validacion fluent
        [TestMethod]
        public void AplicacionConsultaIdCategoriaValidation_Test()
        {
            //Arrange
            var consulta = new Aplicacion.Categorias.ConsultaId.CategoriaUnica();
            consulta.Id_categoria = 1000;

            //Act
            var servicio = new Aplicacion.Categorias.ConsultaId.EjecutaValidacion();
            var servicioRpta = servicio.Validate(consulta);

            //Assert
            Assert.IsTrue(servicioRpta.IsValid);
        }

        //prueba unitaria para validacion fluent
        [TestMethod]
        public void AplicacionConsultaIdDocumentoValidation_Test()
        {
            //Arrange
            var consulta = new Aplicacion.Documentos.ConsultaId.DocumentoUnico();
            consulta.id_documento = 500;

            //Act
            var servicio = new Aplicacion.Documentos.ConsultaId.EjecutaValidacion();
            var servicioRpta = servicio.Validate(consulta);

            //Assert
            Assert.IsTrue(servicioRpta.IsValid);
        }

        //prueba unitaria para validacion fluent
        [TestMethod]
        public void AplicacionConsultaIdPagoValidation_Test()
        {
            //Arrange
            var consulta = new Aplicacion.Pagos.ConsultaId.PagoUnico();
            consulta.Id_pago = 500;

            //Act
            var servicio = new Aplicacion.Pagos.ConsultaId.EjecutaValidacion();
            var servicioRpta = servicio.Validate(consulta);

            //Assert
            Assert.IsTrue(servicioRpta.IsValid);
        }

        //prueba unitaria para validacion fluent
        [TestMethod]
        public void AplicacionLoginUsuarioValidation_Test()
        {
            //Arrange
            var consulta = new Aplicacion.Seguridad.Login.Ejecuta();
            consulta.Email = "RENATO@GMAIL.COM";
            consulta.Password = "$$$$$CONTRASEÑA$$$$";

            //Act
            var servicio = new Aplicacion.Seguridad.Login.EjecutaValidacion();
            var servicioRpta = servicio.Validate(consulta);

            //Assert
            Assert.IsTrue(servicioRpta.IsValid);
        }

        //prueba unitaria para validacion fluent
        [TestMethod]
        public void AplicacionRegistrarUsuarioValidation_Test()
        {
            //Arrange
            var consulta = new Aplicacion.Seguridad.Registrar.Ejecuta();
            consulta.Email = "RENATO@GMAIL.COM";
            consulta.Password = "$$$$$CONTRASEÑA$$$$";
            consulta.Nombre = "RENATO";
            consulta.Username = "RENATO.CHEVA";
            consulta.Apellidos = "CHEVARRIA CAMARGO";

            //Act
            var servicio = new Aplicacion.Seguridad.Registrar.EjecutaValidacion();
            var servicioRpta = servicio.Validate(consulta);

            //Assert
            Assert.IsTrue(servicioRpta.IsValid);
        }

    }
}
