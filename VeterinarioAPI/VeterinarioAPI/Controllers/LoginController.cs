using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VeterinarioAPI.Context;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Controllers
{
    /// <summary>
    /// Autentica usuários e profissionais.
    /// </summary>
    public class LoginController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa a conexão de dados.
        /// </summary>
        public LoginController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Autentica usuários e profissionais.
        /// </summary>
        /// <param name="nome_usuario">Nome de usuário</param>
        /// <param name="senha">Chave de acesso</param>
        /// <returns>Dados de auteticação</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("Login/{nome_usuario}/{senha}")]
        public Login Autenticar(string nome_usuario, string senha)
        {
            var profissional = (from p in _context.Profissionais
                                where p.NomeUsuario.Equals(nome_usuario) &&
                                p.Senha.Equals(senha) &&
                                p.Deleted == false
                                select p).SingleOrDefault();
            if (profissional != null)
                return new Login { Id = profissional.ProfissionalId, NomeUsuario = profissional.NomeUsuario, Senha = profissional.Senha, Tipo = "Profissional" };
            var usuario = (from u in _context.Usuarios
                           where u.NomeUsuario == nome_usuario &&
                           u.Senha == senha &&
                           u.Deleted == false
                           select u).SingleOrDefault();
            if (usuario != null)
                return new Login { Id = usuario.UsuarioId, NomeUsuario = usuario.NomeUsuario, Senha = usuario.Senha, Tipo = "Usuario" };
            return null;
        }
    }
}
