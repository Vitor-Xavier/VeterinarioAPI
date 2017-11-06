using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Contato : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContatoId { get; set; }
        public string Texto { get; set; }
        public bool Principal { get; set; }
        public int TipoContatoId { get; set; }
        public virtual TipoContato TipoContato { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null && typeof(Contato) == obj.GetType())
                return (obj as Contato).ContatoId == ContatoId;
            return false;
        }

        public override int GetHashCode()
        {
            return ContatoId;
        }
    }
}