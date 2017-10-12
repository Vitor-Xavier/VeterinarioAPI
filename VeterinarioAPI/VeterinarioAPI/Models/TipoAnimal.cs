using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class TipoAnimal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoAnimalId { get; set; }
        public string Tipo { get; set; }
    }
}