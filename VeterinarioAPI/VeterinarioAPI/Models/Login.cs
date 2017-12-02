using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Login : EntityBase
    {
        [Required]
        [StringLength(60)]
        [Index(IsUnique = true)]
        public string NomeUsuario { get; set; }
        [Required]
        [StringLength(60)]
        public string Senha { get; set; }
        [NotMapped]
        public int Id { get; set; }
        [NotMapped]
        public string Tipo { get; set; }
    }
}