using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Consulta : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConsultaId { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public int AnimalId { get; set; }
        public virtual Animal Animal { get; set; }
        public int ProfissionalId { get; set; }
        public virtual Profissional Profissional { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null && typeof(Consulta) == obj.GetType())
                return (obj as Consulta).ConsultaId == ConsultaId;
            return false;
        }

        public override int GetHashCode()
        {
            return ConsultaId;
        }
    }
}