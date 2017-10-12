using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Consulta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConsultaId { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int ProfissionalId { get; set; }
        public Profissional Profissional { get; set; }
    }
}