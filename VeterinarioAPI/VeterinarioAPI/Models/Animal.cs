using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Animal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Imagem { get; set; }
        public TipoAnimal TipoAnimal { get; set; }
        public Usuario Dono { get; set; }
    }
}