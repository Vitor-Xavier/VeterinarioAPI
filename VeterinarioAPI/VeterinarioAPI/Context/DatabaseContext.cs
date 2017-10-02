using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=VETERINARIODB") { }

        public virtual DbSet<Animal> Animal { get; set; }
        public virtual DbSet<Consulta> Consulta { get; set; }
        public virtual DbSet<Contato> Contato { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Profissional> Profissional { get; set; }
        public virtual DbSet<Servico> Servico { get; set; }
        public virtual DbSet<TipoAnimal> TipoAnimal { get; set; }
        public virtual DbSet<TipoContato> TipoContato { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}