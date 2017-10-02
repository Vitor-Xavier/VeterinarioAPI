using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class TipoContato
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoContatoId { get; set; }
        public string Nome { get; set; }
    }
}