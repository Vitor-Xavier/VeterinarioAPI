using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinarioAPI.Models
{
    public class Login : EntityBase
    {
        [StringLength(60)]
        [Index(IsUnique = true)]
        public string NomeUsuario { get; set; }
        [StringLength(60)]
        public string Senha { get; set; }
        [NotMapped]
        public int Id { get; set; }
        [NotMapped]
        public string Tipo { get; set; }
    }
}