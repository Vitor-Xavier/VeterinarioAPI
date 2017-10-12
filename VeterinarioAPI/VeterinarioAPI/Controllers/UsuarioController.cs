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
    public class UsuarioController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa a conexão de dados.
        /// </summary>
        public UsuarioController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retorna dados de usuário em específico.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns>Usuário</returns>
        [Route("Usuario/{usuarioId:int}")]
        public Usuario Get(int usuarioId)
        {
            return (from a in _context.Usuario
                    where a.UsuarioId == usuarioId
                    select a).FirstOrDefault();
        }

        /// <summary>
        /// Cria usuário com base nos dados informados.
        /// </summary>
        /// <param name="usuario">Dados do usuário</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Usuario")]
        public IHttpActionResult PostUsuario([FromBody] Usuario usuario)
        {
            try
            {
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
                return Created("Ok", usuario);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Altera usuário com base nos dados informados.
        /// </summary>
        /// <param name="usuario">Dados do usuário</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPut]
        [Route("Usuario")]
        public IHttpActionResult PutUsuario([FromBody] Usuario usuario)
        {
            try
            {
                _context.Usuario.AddOrUpdate(usuario);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Altera estado do usuário para inativo.
        /// </summary>
        /// <param name="usuarioId">Idetificação do usuário</param>
        /// <returns>Sucesso da operação</returns>
        [HttpDelete]
        [Route("Usuario/{usuarioId:int}")]
        public IHttpActionResult DeleteUsuario(int usuarioId)
        {
            try
            {
                _context.Entry(new Usuario { UsuarioId = usuarioId }).State = System.Data.Entity.EntityState.Deleted;
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
