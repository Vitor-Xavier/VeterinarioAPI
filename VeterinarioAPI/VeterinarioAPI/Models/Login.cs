using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Login : EntityBase
    {
        [Index(IsUnique = true)]
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        [NotMapped]
        public int Id { get; set; }
        [NotMapped]
        public string Tipo { get; set; }
    }
}