using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinarioAPI.Models
{
    public class Profissional
    {
        public Profissional()
        {
            //Contatos = new HashSet<Contato>();
            Servicos = new HashSet<Servico>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfissionalId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Imagem { get; set; }
        public string CRV { get; set; }
        public Endereco Endereco { get; set; }
        //public virtual ICollection<Contato> Contatos { get; set; }
        public virtual ICollection<Servico> Servicos { get; set; }

    }
}