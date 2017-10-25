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
    public class ServicoController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa a conexão de dados.
        /// </summary>
        public ServicoController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Lista serviços registrados na base de dados.
        /// </summary>
        /// <returns>Lista de Serviços</returns>
        [HttpGet]
        [Route("Servico")]
        public IEnumerable<Servico> GetAll()
        {
            return _context.Servico;
        }

        /// <summary>
        /// Adiciona serviço.
        /// </summary>
        /// <param name="servico">Dados do serviço</param>
        /// <returns>Secesso da operação</returns>
        [HttpPost]
        [Route("Servico")]
        public IHttpActionResult PostServico([FromBody] Servico servico)
        {
            try
            {
                _context.Servico.Add(servico);
                _context.SaveChanges();

                return Created("Ok", "");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Adiciona serviço ao profissional.
        /// </summary>
        /// <param name="profissionalId">Identificação do profissional</param>
        /// <param name="servico">Serviço a associado</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPut]
        [Route("Servico/{profissionalId:int}")]
        public IHttpActionResult PutServico(int profissionalId, [FromBody] Servico servico)
        {
            try
            {                
                _context.Servico.AddOrUpdate(servico);
                _context.SaveChanges();

                return Created("Ok", profissionalId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Inativa prestação de serviço pelo profissional.
        /// </summary>
        /// <param name="profissionalId">Identificação do profissional</param>
        /// <param name="servicoId">Identificação do serviço</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPut]
        [Route("Servico/{profissionalId:int}/{servicoId:int}")]
        public IHttpActionResult DeleteServico(int profissionalId, int servicoId)
        {
            try
            {
                var servico =  (from s in _context.Servico
                                where s.ServicoId == servicoId
                                select s).SingleOrDefault();

                servico.Profissionais.Remove(servico.Profissionais.Where(p => p.ProfissionalId == profissionalId).SingleOrDefault());
                _context.Servico.AddOrUpdate(servico);
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
