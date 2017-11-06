using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class TipoAnimal : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoAnimalId { get; set; }
        public string Tipo { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null && typeof(TipoAnimal) == obj.GetType())
                return (obj as TipoAnimal).TipoAnimalId == TipoAnimalId;
            return false;
        }

        public override int GetHashCode()
        {
            return TipoAnimalId;
        }
    }
}