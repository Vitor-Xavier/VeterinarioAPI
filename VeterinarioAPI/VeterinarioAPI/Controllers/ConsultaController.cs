using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VeterinarioAPI.Context;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Controllers
{
    public class ConsultaController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa a conexão de dados.
        /// </summary>
        public ConsultaController()
        {
            _context = new DatabaseContext();
        }

        [HttpGet]
        [Route("Consulta/usuario/{usuarioId:int}")]
        public IEnumerable<Consulta> GetByUser(int usuarioId)
        {
            return from c in _context.Consulta
                   where c.Animal.UsuarioId == usuarioId
                   select c;
        }

        [HttpGet]
        [Route("Consulta/animal/{animalId:int}")]
        public IEnumerable<Consulta> GetByAnimal(int animalId)
        {
            return from c in _context.Consulta
                   where c.AnimalId == animalId
                   select c;
        }

        [HttpGet]
        [Route("Consulta/profissional/{profissionalId:int}")]
        public IEnumerable<Consulta> GetByProfissional(int profissionalId)
        {
            return from c in _context.Consulta
                   where c.ProfissionalId == profissionalId
                   select c;
        }

        /// <summary>
        /// Cria usuário com base nos dados informados.
        /// </summary>
        /// <param name="usuario">Dados do usuário</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Consulta")]
        public IHttpActionResult PostConsulta([FromBody] Consulta consulta)
        {
            try
            {
                _context.Consulta.Add(consulta);
                _context.SaveChanges();
                return Created("Ok", consulta);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Altera consulta com base nos dados informados.
        /// </summary>
        /// <param name="consulta">Dados da consulta</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPut]
        [Route("Consulta")]
        public IHttpActionResult PutConsulta([FromBody] Consulta consulta)
        {
            try
            {
                _context.Consulta.AddOrUpdate(consulta);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Altera estado do consulta para inativa.
        /// </summary>
        /// <param name="consultaId">Idetificação da consulta</param>
        /// <returns>Sucesso da operação</returns>
        [HttpDelete]
        [Route("Consulta/{consultaId:int}")]
        public IHttpActionResult DeleteConsulta(int consultaId)
        {
            try
            {
                _context.Entry(new Consulta { ConsultaId = consultaId }).State = System.Data.Entity.EntityState.Deleted;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
