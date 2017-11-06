using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class TipoContato : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoContatoId { get; set; }
        public string Nome { get; set; }
        public string Icone { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null && typeof(TipoContato) == obj.GetType())
                return (obj as TipoContato).TipoContatoId == TipoContatoId;
            return false;
        }

        public override int GetHashCode()
        {
            return TipoContatoId;
        }
    }
}