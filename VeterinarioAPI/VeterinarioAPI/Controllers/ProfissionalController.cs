using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VeterinarioAPI.Context;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Controllers
{
    /// <summary>
    /// Coordena o acesso aos dados de profissionais registrados.
    /// </summary>
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
        /// <param name="latitude">Latitude da localização do usuário</param>
        /// <param name="longitude">Longitude do usuário</param>
        /// <returns>Usuário</returns>
        [HttpGet]
        [Route("Profissional/{latitude:float}/{longitude:float}")]
        public IEnumerable<Profissional> GetByLocation(float latitude, float longitude)
        {
            
            var coord = DbGeography.FromText(String.Format("POINT({0} {1})", latitude.ToString().Replace(",", "."), longitude.ToString().Replace(",", ".")));
            return from p in _context.Profissional
                   orderby p.Endereco.Localizacao.Distance(coord)
                   select p;
        }

        /// <summary>
        /// Retorna dados de profissional em específico.
        /// </summary>
        /// <param name="profissionalId"></param>
        /// <returns>Usuário</returns>
        [Route("Profissional/{profissionalId:int}")]
        public Profissional GetById(int profissionalId)
        {
            return (from p in _context.Profissional
                    where p.ProfissionalId == profissionalId
                    select p).FirstOrDefault();
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
            catch (Exception e)
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

        /// <summary>
        /// Adiciona contato ao profissional.
        /// </summary>
        /// <param name="profissionalId">Identificação do profissional</param>
        /// <param name="contato">Dados de contato</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Profissional/Contato/{profissionalId:int}")]
        public IHttpActionResult PostContatoProfissional(int profissionalId, [FromBody] Contato contato)
        {
            try
            {
                var profissional = (from p in _context.Profissional
                                    where p.ProfissionalId == profissionalId
                                    select p).FirstOrDefault();
                profissional?.Contatos.Add(contato);
                _context.Profissional.AddOrUpdate(profissional);
                _context.SaveChanges();
                return Created("Ok", profissionalId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
