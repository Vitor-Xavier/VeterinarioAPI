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
    /// Realiza ações de leitura e escrita em endereço no banco de dados.
    /// </summary>
    public class EnderecoController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa a conexão de dados.
        /// </summary>
        public EnderecoController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Associa endereço ao profisisonal.
        /// </summary>
        /// <param name="profissionalId">Identificação do profissional</param>
        /// <param name="endereco">Dados do endereço</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Endereco/Profissional/{profissionalId:int}")]
        public IHttpActionResult PostEnderecoProfissional(int profissionalId, [FromBody] Endereco endereco)
        {
            try
            {
                var profissional = (from p in _context.Profissional
                                    where p.ProfissionalId == profissionalId
                                    select p).SingleOrDefault();
                profissional.Endereco = endereco;
                profissional.EnderecoId = endereco.EnderecoId;
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
        /// Associa endereço ao usuário informado.
        /// </summary>
        /// <param name="usuarioId">Identificação do usuário</param>
        /// <param name="endereco">Dados do endereço</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Endereco/Usuario/{usuarioId:int}")]
        public IHttpActionResult PostEnderecoUsuario(int usuarioId, [FromBody] Endereco endereco)
        {
            try
            {
                var usuario = (from u in _context.Usuario
                               where u.UsuarioId == usuarioId
                               select u).SingleOrDefault();
                usuario.Endereco = endereco;
                _context.Usuario.AddOrUpdate(usuario);
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
