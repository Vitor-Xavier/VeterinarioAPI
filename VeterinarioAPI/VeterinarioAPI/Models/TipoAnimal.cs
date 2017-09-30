using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class TipoAnimal
    {
        public int TipoAnimalId { get; set; }
        public string Tipo { get; set; }
        public string Raca { get; set; }
    }
}