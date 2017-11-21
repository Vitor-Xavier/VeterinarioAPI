using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VeterinarioAPI.Context;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Controllers
{
    /// <summary>
    /// Realiza ações de leitura e escrita em endereço no banco de dados.
    /// </summary>
    public class EnderecoController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa a conexão de dados.
        /// </summary>
        public EnderecoController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Associa endereço ao profisisonal.
        /// </summary>
        /// <param name="profissionalId">Identificação do profissional</param>
        /// <param name="endereco">Dados do endereço</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Endereco/Profissional/{profissionalId:int}")]
        public IHttpActionResult PostEnderecoProfissional(int profissionalId, [FromBody] Endereco endereco)
        {
            try
            {
                var profissional = (from p in _context.Profissionais
                                    where p.ProfissionalId == profissionalId
                                    select p).SingleOrDefault();
                profissional.Endereco = endereco;
                profissional.EnderecoId = endereco.EnderecoId;
                _context.Profissionais.AddOrUpdate(profissional);
                _context.SaveChanges();
                return Created("Ok", profissionalId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Associa endereço ao usuário informado.
        /// </summary>
        /// <param name="usuarioId">Identificação do usuário</param>
        /// <param name="endereco">Dados do endereço</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Endereco/Usuario/{usuarioId:int}")]
        public IHttpActionResult PostEnderecoUsuario(int usuarioId, [FromBody] Endereco endereco)
        {
            try
            {
                var usuario = (from u in _context.Usuarios
                               where u.UsuarioId == usuarioId
                               select u).SingleOrDefault();
                usuario.Endereco = endereco;
                _context.Usuarios.AddOrUpdate(usuario);
                _context.SaveChanges();
                return Created("Ok", usuarioId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Busca a latitude e longitude do endereço informada na API de Geocoding da Google.
        /// </summary>
        /// <param name="endereco">Dados do endereço para busca</param>
        /// <returns>Endereço completo</returns>
        [HttpPost]
        [Route("Endereco/LatLng")]
        public async Task<Endereco> GetEnderecoLatLng([FromBody] Endereco endereco)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={endereco}&key=AIzaSyDVpf9a9Zz7LFY6S9j2-jY90fMVgFx3WaU").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                dynamic d = JObject.Parse(await response.Content.ReadAsStringAsync());
                endereco.Latitude = d.results[0].geometry.location.lat;
                endereco.Longitude = d.results[0].geometry.location.lng;
            }
            return endereco;
        }

        /// <summary>
        /// Busca o endereço correspondente as coordenadas informadas.
        /// </summary>
        /// <param name="latitude">Latitude da localização</param>
        /// <param name="longitude">Longitude da localização</param>
        /// <returns>Endereço completo</returns>
        [HttpGet]
        [Route("Endereco/Completo/{latitude:double}/{longitude:double}/")]
        public async Task<Endereco> GetEnderecoCompleto(double latitude, double longitude)
        {
            Endereco endereco = new Endereco();

            HttpClient client = new HttpClient();
            var lat = latitude.ToString(CultureInfo.CreateSpecificCulture("en-US"));
            var lng = longitude.ToString(CultureInfo.CreateSpecificCulture("en-US"));
            var t = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}&key=AIzaSyDVpf9a9Zz7LFY6S9j2-jY90fMVgFx3WaU";
            var response = await client.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}&key=AIzaSyDVpf9a9Zz7LFY6S9j2-jY90fMVgFx3WaU").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                dynamic d = JObject.Parse(await response.Content.ReadAsStringAsync());
                endereco.Numero = d.results[0].address_components[0].long_name;
                endereco.Logradouro = d.results[0].address_components[1].long_name;
                endereco.Bairro = d.results[0].address_components[2].long_name;
                endereco.Cidade = d.results[0].address_components[3].long_name;
                endereco.Estado = d.results[0].address_components[5].long_name;
                endereco.CEP = d.results[0].address_components[7].long_name;
                endereco.Latitude = d.results[0].geometry.location.lat;
                endereco.Longitude = d.results[0].geometry.location.lng;
                endereco.Complemento = string.Empty;
            }
            return endereco;
        }
    }
}
