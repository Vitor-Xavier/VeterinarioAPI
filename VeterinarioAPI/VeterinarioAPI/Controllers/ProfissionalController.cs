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
    public class ProfissionalController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa a conexão de dados.
        /// </summary>
        public ProfissionalController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retorna dados de profissionais.
        /// </summary>
        /// <returns>Lista de Profissionais</returns>
        [Route("Profissional")]
        public IEnumerable<Profissional> Get()
        {
            return _context.Profissional;
        }

        /// <summary>
        /// Retorna dados de profissional em específico.
        /// </summary>
        /// <param name="profissionalId"></param>
        /// <returns>Usuário</returns>
        [Route("Profissional/{profissionalId:int}")]
        public Profissional GetById(int profissionalId)
        {
            return (from a in _context.Profissional
                    where a.ProfissionalId == profissionalId
                    select a).FirstOrDefault();
        }

        /// <summary>
        /// Cria profissional com base nos dados informados.
        /// </summary>
        /// <param name="profissional">Dados do usuário</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Profissional")]
        public IHttpActionResult PostProfissional([FromBody] Profissional profissional)
        {
            try
            {
                _context.Profissional.Add(profissional);
                _context.SaveChanges();
                return Created("Ok", profissional);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Altera profissional com base nos dados informados.
        /// </summary>
        /// <param name="profissional">Dados do profissional</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPut]
        [Route("Profissional")]
        public IHttpActionResult PutProfissional([FromBody] Profissional profissional)
        {
            try
            {
                _context.Profissional.AddOrUpdate(profissional);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Altera estado do profissional para inativo.
        /// </summary>
        /// <param name="profissionalId">Idetificação do profissional</param>
        /// <returns>Sucesso da operação</returns>
        [HttpDelete]
        [Route("Profissional/{profissionalId:int}")]
        public IHttpActionResult DeleteProfissional(int profissionalId)
        {
            try
            {
                _context.Entry(new Profissional { ProfissionalId = profissionalId }).State = System.Data.Entity.EntityState.Deleted;
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
