using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Servico
    {
        public int ServicoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}