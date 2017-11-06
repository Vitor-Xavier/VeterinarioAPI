using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Usuario : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Imagem { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<Contato> Contatos { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null && typeof(Usuario) == obj.GetType())
                return (obj as Usuario).UsuarioId == UsuarioId;
            return false;
        }

        public override int GetHashCode()
        {
            return UsuarioId;
        }
    }
}