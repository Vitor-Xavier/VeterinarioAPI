using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using VeterinarioAPI.Context;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Controllers
{
    /// <summary>
    /// Classe que controla o acesso aos dados de animais.
    /// </summary>
    public class AnimalController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa a conexão de dados.
        /// </summary>
        public AnimalController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retona lita de animais associados a determinado usuário.
        /// </summary>
        /// <param name="usuarioId">Identificação do usuário</param>
        /// <returns>Lista de Animais</returns>
        [Route("Animal/{usuarioId:int}")]
        public IEnumerable<Animal> Get(int usuarioId)
        {
            return from a in _context.Animal
                   where a.UsuarioId == usuarioId //&&
                   //a.Deleted == false
                   select a;
        }

        /// <summary>
        /// Retorna animal correspodente a identificação informada.
        /// </summary>
        /// <param name="usuarioId">Identificação do usuário</param>
        /// <param name="animalId">Identificação do animal</param>
        /// <returns>Animal</returns>
        [Route("Animal/{usuarioId:int}/{animalId:int}")]
        public Animal GetById(int usuarioId, int animalId)
        {
            return (from a in _context.Animal
                    where a.UsuarioId == usuarioId && 
                    a.AnimalId == animalId &&
                    a.Deleted == false
                    select a).FirstOrDefault();
        }

        /// <summary>
        /// Pesquisa animais de determinada pessoa pelo nome.
        /// </summary>
        /// <param name="usuarioId">Identificação do usuário</param>
        /// <param name="nome">Nome do animal para pesquisa</param>
        /// <returns></returns>
        public IEnumerable<Animal> GetByName(int usuarioId, string nome)
        {
            return from a in _context.Animal
                   where a.UsuarioId == usuarioId &&
                   a.Nome.Contains(nome) &&
                   a.Deleted == false
                   select a;
        }

        /// <summary>
        /// Adiciona animal e o associa ao dono.
        /// </summary>
        /// <param name="usuarioId">Identificação do usuário</param>
        /// <param name="animal">Dados do animal</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Animal/{usuarioId:int}")]
        public IHttpActionResult PostAnimal(int usuarioId, [FromBody] Animal animal)
        {
            try
            {
                animal.UsuarioId = usuarioId;
                _context.Animal.Add(animal);
                _context.SaveChanges();
                return Created(usuarioId.ToString(), animal);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Altera dados de animal cadastrado
        /// </summary>
        /// <param name="usuarioId">Identificação do usuário</param>
        /// <param name="animal">Dados do animal</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPut]
        [Route("Animal/{usuarioId:int}")]
        public IHttpActionResult PutAnimal(int usuarioId, [FromBody] Animal animal)
        {
            try
            {
                _context.Animal.AddOrUpdate(animal);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Muda estado do animal para inativo.
        /// </summary>
        /// <param name="usuarioId">Identificação do usuário</param>
        /// <param name="animalId">Identificação do animal</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Animal/{usuarioId:int}/{animalId:int}")]
        public IHttpActionResult DeleteAnimal(int usuarioId, int animalId)
        {
            try
            {
                _context.Animal.AddOrUpdate(new Animal { AnimalId = animalId, Deleted = true });
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
