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
    /// <summary>
    /// Atualiza ou inativa contato de usuários e profissionais.
    /// </summary>
    public class ContatoController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa a conexão de dados.
        /// </summary>
        public ContatoController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Altera contato.
        /// </summary>
        /// <param name="contato">Dados de contato</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPut]
        [Route("Contato/Profissional")]
        public IHttpActionResult PutContato([FromBody] Contato contato)
        {
            try
            {
                _context.Contato.AddOrUpdate(contato);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Inativa contato.
        /// </summary>
        /// <param name="contatoId">Identificação do contato</param>
        /// <returns>Sucesso da operação</returns>
        [HttpDelete]
        [Route("Contato/Profissional/{contatoId:int}")]
        public IHttpActionResult DeleteContato(int contatoId)
        {
            try
            {
                _context.Entry(new Contato { ContatoId = contatoId }).State = System.Data.Entity.EntityState.Deleted;
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
