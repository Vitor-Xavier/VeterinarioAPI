using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace VeterinarioAPI.Models
{
    public class Profissional : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfissionalId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Imagem { get; set; }
        public string Icone { get; set; }
        public string CRV { get; set; }
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<Contato> Contatos { get; set; }
        public virtual ICollection<Servico> Servicos { get; set; }

        public override bool Equals(object obj)
        {
            if(obj != null && typeof(Profissional) == obj.GetType())
                return (obj as Profissional).ProfissionalId == ProfissionalId;
            return false;
        }

        public override int GetHashCode()
        {
            return ProfissionalId;
        }
    }
}