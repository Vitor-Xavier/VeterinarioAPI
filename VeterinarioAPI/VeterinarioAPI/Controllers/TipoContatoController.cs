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
    public class TipoContatoController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa a conexão de dados.
        /// </summary>
        public TipoContatoController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retorna todos os tipos de contato registrados.
        /// </summary>
        /// <returns>Lista de Tipos de Contato</returns>
        [HttpGet]
        [Route("TipoContato")]
        public IEnumerable<TipoContato> GetAll()
        {
            return from ta in _context.TipoContatos
                   where ta.Deleted == false
                   select ta;
        }

        /// <summary>
        /// Retorna tipo de contato em específico.
        /// </summary>
        /// <returns>Lista de Tipos de Contato</returns>
        [HttpGet]
        [Route("TipoContato/{tipoContatoId:int}")]
        public IEnumerable<TipoContato> GetById(int tipoContatoId)
        {
            return from ta in _context.TipoContatos
                   where ta.Deleted == false &&
                   ta.TipoContatoId == tipoContatoId
                   select ta;
        }

        /// <summary>
        /// Adiciona tipo de contato no banco de dados.
        /// </summary>
        /// <param name="tipoContato">Dados do tipo de contato</param>
        /// <returns>Sucesso da Operação</returns>
        [HttpPost]
        [Route("TipoContato")]
        public IHttpActionResult PostTipoAnimal([FromBody] TipoContato tipoContato)
        {
            try
            {
                _context.TipoContatos.Add(tipoContato);
                _context.SaveChanges();

                return Created("Ok", tipoContato);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Altera dados de tipo de contato.
        /// </summary>
        /// <param name="tipoContato">Dados do tipo de contato</param>
        /// <returns>Sucesso da Operação</returns>
        [HttpPut]
        [Route("TipoContato")]
        public IHttpActionResult PutTipoAnimal([FromBody] TipoContato tipoContato)
        {
            try
            {
                _context.TipoContatos.AddOrUpdate(tipoContato);
                _context.SaveChanges();

                return Ok("Ok");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Inativo tipo de contato informado.
        /// </summary>
        /// <param name="tipoContatoId">Identificação do tipo de contato</param>
        /// <returns>Sucesso da Operação</returns>
        [HttpDelete]
        [Route("TipoContato/{tipoAnimalId:int}")]
        public IHttpActionResult DeleteTipoAnimal(int tipoContatoId)
        {
            try
            {
                var tipoContato = new TipoContato { TipoContatoId = tipoContatoId, Deleted = true };
                _context.TipoContatos.Attach(tipoContato);
                _context.Entry(tipoContato).Property(x => x.Deleted).IsModified = true;
                _context.SaveChanges();

                return Ok("Ok");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
