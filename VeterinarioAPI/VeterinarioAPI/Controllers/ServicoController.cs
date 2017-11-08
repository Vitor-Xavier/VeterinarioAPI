using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using VeterinarioAPI.Context;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Controllers
{
    /// <summary>
    /// Manuseia o base de dados buscando e alterando serviços.
    /// </summary>
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
            return _context.Servicos;
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
                _context.Servicos.Add(servico);
                _context.SaveChanges();

                return Created("Ok", "");
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Altera dados de serviço.
        /// </summary>
        /// <param name="servico">Dados do serviço</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPut]
        [Route("Servico")]
        public IHttpActionResult PutServico([FromBody] Servico servico)
        {
            try
            {                
                _context.Servicos.AddOrUpdate(servico);
                _context.SaveChanges();

                return Created("Ok", servico);
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
                var servico =  (from s in _context.Servicos
                                where s.ServicoId == servicoId
                                select s).SingleOrDefault();

                servico.Profissionais.Remove(servico.Profissionais.Where(p => p.ProfissionalId == profissionalId).SingleOrDefault());
                _context.Servicos.AddOrUpdate(servico);
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
