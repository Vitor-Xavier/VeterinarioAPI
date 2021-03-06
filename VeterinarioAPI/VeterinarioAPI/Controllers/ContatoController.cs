﻿using System;
using System.Linq;
using System.Data.Entity.Migrations;
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
        [Route("Contato")]
        public IHttpActionResult PutContato([FromBody] Contato contato)
        {
            try
            {
                contato.TipoContato = null;
                _context.Contatos.AddOrUpdate(contato);
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
        [Route("Contato/{contatoId:int}")]
        public IHttpActionResult DeleteContato(int contatoId)
        {
            try
            {
                var contato = new Contato { ContatoId = contatoId, Deleted = true };
                _context.Contatos.Attach(contato);
                _context.Entry(contato).Property(x => x.Deleted).IsModified = true;

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
