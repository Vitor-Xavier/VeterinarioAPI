using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Imagem { get; set; }
        public Endereco Endereco { get; set; }
        public List<Contato> Contato { get; set; }
    }
}