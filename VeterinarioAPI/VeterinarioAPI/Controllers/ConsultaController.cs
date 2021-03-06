﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using VeterinarioAPI.Context;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Controllers
{
    /// <summary>
    /// Classe responsável por gerenciar o acesso a dados relacionados a consultas.
    /// </summary>
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

        /// <summary>
        /// Retorna consultas envolvendo os animais do usuário informado, e data caso informado.
        /// </summary>
        /// <param name="usuarioId">Identificação do usuário para busca</param>
        /// <param name="dataIni">Data de inicio para busca, opcional</param>
        /// <param name="dataFim">Data de término para busca, opcional</param>
        /// <returns>Lista de Consultas</returns>
        [HttpGet]
        [Route("Consulta/usuario/{usuarioId:int}")]
        [Route("Consulta/usuario/{usuarioId:int}/{dataIni:datetime?}/")]
        [Route("Consulta/usuario/{usuarioId:int}/{dataIni:datetime?}/{dataFim:datetime?}/")]
        public IEnumerable<Consulta> GetByUsuario(int usuarioId, DateTime? dataIni = null, DateTime? dataFim = null)
        {
            return (from c in _context.Consultas
                    where c.Animal.UsuarioId == usuarioId &&
                    (dataIni != null ? dataIni >= c.Data : true) &&
                    (dataFim != null ? dataFim <= c.Data : true) &&
                    c.Deleted == false
                    select c).AsEnumerable();
        }

        /// <summary>
        /// Retorna consultas envolvendo os animais do usuário informado, e data caso informado.
        /// </summary>
        /// <param name="usuarioId">Identificação do usuário para busca</param>
        /// <param name="nome">Nome do profissional ou animal para busca</param>
        /// <param name="dataIni">Data de inicio para busca, opcional</param>
        /// <param name="dataFim">Data de término para busca, opcional</param>
        /// <returns>Lista de Consultas</returns>
        [HttpGet]
        [Route("Consulta/usuario/pesquisa/{usuarioId:int}/{nome}")]
        [Route("Consulta/usuario/pesquisa/{usuarioId:int}/{nome}/{dataIni:datetime?}/")]
        [Route("Consulta/usuario/pesquisa/{usuarioId:int}/{nome}/{dataIni:datetime?}/{dataFim:datetime?}/")]
        public IEnumerable<Consulta> GetByUsuarioNome(int usuarioId, string nome, DateTime? dataIni = null, DateTime? dataFim = null)
        {
            return from c in _context.Consultas
                   where c.Animal.UsuarioId == usuarioId &&
                   (c.Animal.Nome.Contains(nome) ||
                   c.Profissional.Nome.Contains(nome)) &&
                   (dataIni != null ? dataIni >= c.Data : true) &&
                   (dataFim != null ? dataFim <= c.Data : true) &&
                   c.Deleted == false
                   select c;
        }

        /// <summary>
        /// Retorna consultas marcadas para determinado animal, e data caso informado.
        /// </summary>
        /// <param name="animalId">Identificação do animal para busca</param>
        /// <param name="dataIni">Data de inicio para busca, opcional</param>
        /// <param name="dataFim">Data de término para busca, opcional</param>
        /// <returns>Lista de Consultas</returns>
        [HttpGet]
        [Route("Consulta/animal/{animalId:int}")]
        [Route("Consulta/animal/{animalId:int}/{dataIni:datetime?}/")]
        [Route("Consulta/animal/{animalId:int}/{dataIni:datetime?}/{dataFim:datetime?}/")]
        public IEnumerable<Consulta> GetByAnimal(int animalId, DateTime? dataIni = null, DateTime? dataFim = null)
        {
            return from c in _context.Consultas
                   where c.AnimalId == animalId &&
                   (dataIni != null ? dataIni >= c.Data : true) &&
                   (dataFim != null ? dataFim <= c.Data : true) &&
                   c.Deleted == false
                   select c;
        }

        /// <summary>
        /// Retorna consultas marcadas para determinado animal, e data caso informado.
        /// </summary>
        /// <param name="animalId">Identificação do animal para busca</param>
        /// <param name="nome">Texto para busca em nome do animal</param>
        /// <param name="dataIni">Data de inicio para busca, opcional</param>
        /// <param name="dataFim">Data de término para busca, opcional</param>
        /// <returns>Lista de Consultas</returns>
        [HttpGet]
        [Route("Consulta/animal/pesquisa/{animalId:int}/{nome}")]
        [Route("Consulta/animal/pesquisa/{animalId:int}/{nome}/{dataIni:datetime?}/")]
        [Route("Consulta/animal/pesquisa/{animalId:int}/{nome}/{dataIni:datetime?}/{dataFim:datetime?}/")]
        public IEnumerable<Consulta> GetByAnimal(int animalId, string nome, DateTime? dataIni = null, DateTime? dataFim = null)
        {
            return from a in _context.Consultas
                   where a.AnimalId == animalId &&
                   a.Animal.Nome.ToLower().Contains(nome.ToLower()) &&
                   (dataIni != null ? dataIni >= a.Data : true) &&
                   (dataFim != null ? dataFim <= a.Data : true) &&
                   a.Deleted == false
                   select a;
        }

        /// <summary>
        /// Retorna consultas agendadas com o profissional informado, e data caso informado.
        /// </summary>
        /// <param name="profissionalId">Identificação do profissional para busca</param>
        /// <param name="dataIni">Data de inicio para busca, opcional</param>
        /// <param name="dataFim">Data de término para busca, opcional</param>
        /// <returns>Lista de Consultas</returns>
        [HttpGet]
        [Route("Consulta/profissional/{profissionalId:int}")]
        [Route("Consulta/profissional/{profissionalId:int}/{dataIni:datetime?}/")]
        [Route("Consulta/profissional/{profissionalId:int}/{dataIni:datetime?}/{dataFim:datetime?}/")]
        public IEnumerable<Consulta> GetByProfissional(int profissionalId, DateTime? dataIni = null, DateTime? dataFim = null)
        {
            return (from c in _context.Consultas
                    where c.ProfissionalId == profissionalId &&
                    (dataIni != null ? dataIni >= c.Data : true) &&
                    (dataFim != null ? dataFim <= c.Data : true) &&
                    c.Deleted == false
                    select c).AsEnumerable();
        }

        /// <summary>
        /// Retorna consultas agendadas com o profissional informado, e data caso informado.
        /// </summary>
        /// <param name="profissionalId">Identificação do profissional para busca</param>
        /// <param name="nome">Nome do dono ou animal para busca</param>
        /// <param name="dataIni">Data de inicio para busca, opcional</param>
        /// <param name="dataFim">Data de término para busca, opcional</param>
        /// <returns>Lista de Consultas</returns>
        [HttpGet]
        [Route("Consulta/profissional/pesquisa/{profissionalId:int}/{nome}")]
        [Route("Consulta/profissional/pesquisa/{profissionalId:int}/{nome}/{dataIni:datetime?}/")]
        [Route("Consulta/profissional/pesquisa/{profissionalId:int}/{nome}/{dataIni:datetime?}/{dataFim:datetime?}/")]
        public IEnumerable<Consulta> GetByProfissionalNome(int profissionalId, string nome, DateTime? dataIni = null, DateTime? dataFim = null)
        {
            return from c in _context.Consultas
                   where c.ProfissionalId == profissionalId &&
                   (c.Animal.Nome.Contains(nome) ||
                   c.Animal.Usuario.Nome.Contains(nome)) &&
                   (dataIni != null ? dataIni >= c.Data : true) &&
                   (dataFim != null ? dataFim <= c.Data : true) &&
                   c.Deleted == false
                   select c;
        }

        /// <summary>
        /// Cria consulta com base nos dados informados.
        /// </summary>
        /// <param name="consulta">Dados da consulta a ser cadastrada</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Consulta")]
        public IHttpActionResult PostConsulta([FromBody] Consulta consulta)
        {
            try
            {
                consulta.Profissional = null;
                consulta.Animal = null;
                _context.Consultas.Add(consulta);
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
                consulta.Profissional = null;
                consulta.Animal = null;
                _context.Consultas.AddOrUpdate(consulta);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Altera o estado da consulta informada.
        /// </summary>
        /// <param name="consultaId">Identificação da consulta</param>
        /// <param name="status">Estado da requisição de consulta</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPut]
        [Route("Consulta/Requisicao/{consultaId:int}/{status:int}")]
        public IHttpActionResult PutStatusConsulta(int consultaId, int status)
        {
            try
            {
                var consulta = new Consulta { ConsultaId = consultaId, Status = status };
                _context.Consultas.Attach(consulta);
                _context.Entry(consulta).Property(x => x.Status).IsModified = true;

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
                var consulta = new Consulta { ConsultaId = consultaId, Deleted = true };
                _context.Consultas.Attach(consulta);
                _context.Entry(consulta).Property(x => x.Deleted).IsModified = true;

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
