using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Contato
    {
        public int ContatoId { get; set; }
        public string Texto { get; set; }
        public TipoContato TipoContato { get; set; }
    }
}