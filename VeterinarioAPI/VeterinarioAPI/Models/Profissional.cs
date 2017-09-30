using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Profissional
    {
        public int ProfissionalId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Imagem { get; set; }
        public string CRV { get; set; }
        public Endereco Endereco { get; set; }
        public List<Contato> Contatos { get; set; }
        public List<Servico> Servicos { get; set; }

    }
}