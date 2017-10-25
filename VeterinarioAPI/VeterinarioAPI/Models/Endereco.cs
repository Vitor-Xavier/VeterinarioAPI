using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace VeterinarioAPI.Models
{
    public class Endereco
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

        private DbGeography localizacao;
        [JsonIgnore]
        public DbGeography Localizacao
        {
            get
            {
                return DbGeography.FromText(String.Format("POINT({0} {1})", Latitude.ToString().Replace(",", "."), Longitude.ToString().Replace(",", ".")));
            }
            set
            {
                localizacao = DbGeography.FromText(String.Format("POINT({0} {1})", Latitude.ToString().Replace(",", "."), Longitude.ToString().Replace(",", ".")));
            }
        }

    }
}