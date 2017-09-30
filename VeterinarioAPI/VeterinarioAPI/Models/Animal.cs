using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Imagem { get; set; }
        public TipoAnimal TipoAnimal { get; set; }
        public Usuario Dono { get; set; }
    }
}