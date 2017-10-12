using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Contato
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContatoId { get; set; }
        public string Texto { get; set; }
        public bool Principal { get; set; }
        public int TipoContatoId { get; set; }
        public TipoContato TipoContato { get; set; }
    }
}