using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Servico
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServicoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool RequerCRV { get; set; }
        public virtual ICollection<Profissional> Profissionais { get; set; }
    }
}