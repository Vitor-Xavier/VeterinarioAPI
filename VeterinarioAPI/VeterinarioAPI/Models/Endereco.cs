using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Endereco : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnderecoId { get; set; }
        public int Numero { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null && typeof(Endereco) == obj.GetType())
                return (obj as Endereco).EnderecoId == EnderecoId;
            return false;
        }

        public override int GetHashCode()
        {
            return EnderecoId;
        }

        public override string ToString()
        {
            return $"{Logradouro}, {Numero} - {Bairro}, {Cidade} - {Estado}, {CEP}";
        }
    }
}