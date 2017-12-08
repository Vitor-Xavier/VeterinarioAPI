using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Animal : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Imagem { get; set; }
        public int TipoAnimalId { get; set; }
        public virtual TipoAnimal TipoAnimal { get; set; }
        public int UsuarioId { get; set; }
        [JsonProperty("dono")]
        public virtual Usuario Usuario { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null && typeof(Animal) == obj.GetType())
                return (obj as Animal).AnimalId == AnimalId;
            return false;
        }

        public override int GetHashCode()
        {
            return AnimalId;
        }
    }
}