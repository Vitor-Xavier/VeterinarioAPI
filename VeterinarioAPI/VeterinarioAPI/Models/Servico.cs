using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Servico : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServicoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool RequerCRV { get; set; }
        [JsonIgnore]
        public virtual ICollection<Profissional> Profissionais { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null && typeof(Servico) == obj.GetType())
                return (obj as Servico).ServicoId == ServicoId;
            return false;
        }

        public override int GetHashCode()
        {
            return ServicoId;
        }
    }
}