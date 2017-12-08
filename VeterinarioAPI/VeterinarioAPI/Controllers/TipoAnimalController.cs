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
    /// Gerencia requisições por tipo de animais.
    /// </summary>
    public class TipoAnimalController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa a conexão de dados.
        /// </summary>
        public TipoAnimalController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retorna todos os tipos de animais registrados.
        /// </summary>
        /// <returns>Lista de Tipos de Animais</returns>
        [HttpGet]
        [Route("TipoAnimal")]
        public IEnumerable<TipoAnimal> GetAll()
        {
            return (from ta in _context.TipoAnimais
                    where ta.Deleted == false
                    select ta).AsEnumerable();
        }

        /// <summary>
        /// Retorna tipo de animais em específico.
        /// </summary>
        /// <returns>Lista de Tipos de Animais</returns>
        [HttpGet]
        [Route("TipoAnimal/{tipoAnimalId:int}")]
        public IEnumerable<TipoAnimal> GetById(int tipoAnimalId)
        {
            return from ta in _context.TipoAnimais
                   where ta.Deleted == false &&
                   ta.TipoAnimalId == tipoAnimalId
                   select ta;
        }

        /// <summary>
        /// Adiciona tipo de animal no banco de dados.
        /// </summary>
        /// <param name="tipoAnimal">Dados do tipo de animal</param>
        /// <returns>Sucesso da Operação</returns>
        [HttpPost]
        [Route("TipoAnimal")]
        public IHttpActionResult PostTipoAnimal([FromBody] TipoAnimal tipoAnimal)
        {
            try
            {
                _context.TipoAnimais.Add(tipoAnimal);
                _context.SaveChanges();

                return Created("Ok", tipoAnimal);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Altera dados de tipo de animal.
        /// </summary>
        /// <param name="tipoAnimal">Dados do tipo de animal</param>
        /// <returns>Sucesso da Operação</returns>
        [HttpPut]
        [Route("TipoAnimal")]
        public IHttpActionResult PutTipoAnimal([FromBody] TipoAnimal tipoAnimal)
        {
            try
            {
                _context.TipoAnimais.AddOrUpdate(tipoAnimal);
                _context.SaveChanges();

                return Ok("Ok");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Inativo tipo de animal informado.
        /// </summary>
        /// <param name="tipoAnimalId">Identificação do tipo de animal</param>
        /// <returns>Sucesso da Operação</returns>
        [HttpDelete]
        [Route("TipoAnimal/{tipoAnimalId:int}")]
        public IHttpActionResult DeleteTipoAnimal(int tipoAnimalId)
        {
            try
            {
                var tipoAnimal = new TipoAnimal { TipoAnimalId = tipoAnimalId, Deleted = true };
                _context.TipoAnimais.Attach(tipoAnimal);
                _context.Entry(tipoAnimal).Property(x => x.Deleted).IsModified = true;

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
