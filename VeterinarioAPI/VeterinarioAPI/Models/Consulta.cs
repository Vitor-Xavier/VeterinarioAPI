using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Consulta
    {
        public int ConsultaId { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public Animal Animal { get; set; }
        public Profissional Profissional { get; set; }
    }
}