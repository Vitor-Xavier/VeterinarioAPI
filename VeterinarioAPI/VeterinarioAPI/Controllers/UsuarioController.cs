using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using VeterinarioAPI.Context;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Controllers
{
    /// <summary>
    /// Gerencia as requisições sobre dados de usuários.
    /// </summary>
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
        [HttpGet]
        [Route("Usuario/{usuarioId:int}")]
        public Usuario Get(int usuarioId)
        {
            return (from a in _context.Usuarios
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
                _context.Usuarios.Add(usuario);
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
                _context.Usuarios.AddOrUpdate(usuario);
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
                _context.Usuarios.AddOrUpdate(new Usuario { UsuarioId = usuarioId, Deleted = true });
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Adiciona contato ao usuário informado.
        /// </summary>
        /// <param name="usuarioId">Identificação do usuário</param>
        /// <param name="contato">Dados de contato</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Usuario/Contato/{usuarioId:int}")]
        public IHttpActionResult PostContatoUsuario(int usuarioId, [FromBody] Contato contato)
        {
            try
            {
                var usuario = (from u in _context.Usuarios
                               where u.UsuarioId == usuarioId
                               select u).SingleOrDefault();
                usuario?.Contatos.Add(contato);
                _context.Usuarios.AddOrUpdate(usuario);
                _context.SaveChanges();

                return Created("Ok", usuarioId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

    }
}
