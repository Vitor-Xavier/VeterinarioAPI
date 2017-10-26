using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web.Http;
using VeterinarioAPI.Context;
using VeterinarioAPI.Models;
using VeterinarioAPI.Utils;

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
        [Route("Profissional/{latitude:double}/{longitude:double}/")]
        public IEnumerable<Profissional> GetByLocation(double latitude, double longitude)
        {
            try
            {
                var coord = DbGeography.FromText(String.Format("POINT({0} {1})", latitude.ToString().Replace(",", "."), longitude.ToString().Replace(",", ".")));
                var tst = from p in _context.Profissional
                          let coord2 = DbGeography.FromText("POINT(" + p.Endereco.Latitude + " " + p.Endereco.Longitude + ")")
                          orderby coord2.Distance(coord)
                          select p;
                return tst;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// Retorna profissionais que prestam determinado serviço.
        /// </summary>
        /// <param name="servicoId">Identificação do serviço</param>
        /// <returns>Lista de Profissionais</returns>
        [HttpGet]
        [Route("Profissional/Servico")]
        public IEnumerable<Profissional> GetByServico(int servicoId)
        {
            return  from p in _context.Profissional
                    where p.Servicos.Any(s => s.ServicoId == servicoId)
                    select p;
        }

        [HttpPost]
        [Route("Profissional/Servicos/Any/{latitude:double}/{longitude:double}/")]
        public IEnumerable<Profissional> GetByAnyServicos(double latitude, double longitude, [FromBody] IEnumerable<Servico> servicos)
        {
            //var query3 = prof.Where(p => p.Servicos.Any(c => servicos.Any(c2 => c2.ServicoId == c.ServicoId)));

            //var tst = prof.Any(p => p.Servicos.Intersect(servicos, new ServicoComparer()))

            var coord = DbGeography.FromText(String.Format("POINT({0} {1})", latitude.ToString().Replace(",", "."), longitude.ToString().Replace(",", ".")));
            return from p in _context.Profissional.AsEnumerable<Profissional>()
                   //let coord2 = DbGeography.FromText("POINT(" + p.Endereco.Latitude + " " + p.Endereco.Longitude + ")")
                   where p.Servicos.Any(s1 => servicos.Any(s2 => s2.ServicoId == s1.ServicoId))
                   //orderby coord2.Distance(coord)
                   select p;
        }

        [HttpPost]
        [Route("Profissional/Servicos/All/{latitude:double}/{longitude:double}/")]
        public IEnumerable<Profissional> GetByAllServicos(double latitude, double longitude, [FromBody] IEnumerable<Servico> servicos)
        {
            //var query4 = prof.Where(p => p.Servicos.Where(c => servicos.Any(c2 => c2.ServicoId == c.ServicoId)).Count() == servicos.Count());

            var coord = DbGeography.FromText(String.Format("POINT({0} {1})", latitude.ToString().Replace(",", "."), longitude.ToString().Replace(",", ".")));
            var tst = from p in _context.Profissional.AsEnumerable<Profissional>()
                      //let coord2 = DbGeography.FromText("POINT(" + p.Endereco.Latitude + " " + p.Endereco.Longitude + ")")
                      where p.Servicos.Where(c => servicos.Any(c2 => c2.ServicoId == c.ServicoId)).Count() == servicos.Count()
                      //orderby coord2.Distance(coord)
                      select p;
            return tst;
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
                var tipoContato = (from tc in _context.TipoContato
                                   where tc.TipoContatoId == contato.TipoContatoId
                                   select tc).FirstOrDefault();
                contato.TipoContato = tipoContato ?? contato.TipoContato;
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

        /// <summary>
        /// Associa serviço ao profissional.
        /// </summary>
        /// <param name="profissionalId">Identificação do profissional</param>
        /// <param name="servico">Dados do serviço</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Profissional/Servico/{profissionalId:int}")]
        public IHttpActionResult PostServicoProfissional(int profissionalId, [FromBody] Servico servico)
        {
            try
            {
                var profissional = (from p in _context.Profissional
                                    where p.ProfissionalId == profissionalId
                                    select p).FirstOrDefault();
                var serv = (from s in _context.Servico
                            where s.ServicoId == servico.ServicoId
                            select s).Single();
                servico = serv ?? servico;
                profissional?.Servicos.Add(serv);
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
