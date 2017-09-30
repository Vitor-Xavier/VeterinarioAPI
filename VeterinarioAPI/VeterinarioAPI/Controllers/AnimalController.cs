using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Controllers
{
    public class AnimalController : ApiController
    {
        [Route("api/Animal/get")]
        public List<Animal> Get()
        {
            return new List<Animal>
            {
                new Animal
                {
                    AnimalId = 1,
                    Nome = "Scooby-Doo",
                    DataNascimento = new DateTime(2006, 10, 1),
                    TipoAnimal = new TipoAnimal{TipoAnimalId = 1, Tipo = "Cachorro", Raca = "Dachshund" },
                    Dono = new Usuario{Nome = "Vitor", Sobrenome = "Xavier", UsuarioId = 1, Contato = new List<Contato> { new Contato {ContatoId = 1, Texto = "vitorvxs@gmail.com", TipoContato = new TipoContato {TipoContatoId = 1, Nome = "Email" } } }, Endereco = new Endereco {Logradouro = "Rua Arnaldo Victaliano", Bairro = "Iguatemi", Numero = 1610, Complemento = "Ap. 51-A", Estado = "SP", Cidade = "Ribeirão Preto", CEP = "14091220", EnderecoId = 1 } } },
                 new Animal
                {
                    AnimalId = 1,
                    Nome = "Triss",
                    DataNascimento = new DateTime(2006, 10, 1),
                    TipoAnimal = new TipoAnimal{TipoAnimalId = 2, Tipo = "Gato", Raca = "Gato" },
                    Dono = new Usuario{ Nome = "Vitor", Sobrenome = "Xavier", UsuarioId = 1, Contato = new List<Contato> { new Contato {ContatoId = 1, Texto = "vitorvxs@live.com", TipoContato = new TipoContato {TipoContatoId = 1, Nome = "Email" } } }, Endereco = new Endereco {Logradouro = "Rua Rio Branco", Bairro = "Americanópolis", Numero = 65, Complemento = null, Estado = "SP", Cidade = "São Paulo", CEP = "14091220", EnderecoId = 1 } } },
            };
        }
    }
}
