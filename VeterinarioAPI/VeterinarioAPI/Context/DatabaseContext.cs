using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VeterinarioAPI.Controllers;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=Veterinario_DB") { } //base("name=VETERINARIODB") { }

        public virtual DbSet<Animal> Animais { get; set; }
        public virtual DbSet<Consulta> Consultas { get; set; }
        public virtual DbSet<Contato> Contatos { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
        public virtual DbSet<Profissional> Profissionais { get; set; }
        public virtual DbSet<Servico> Servicos { get; set; }
        public virtual DbSet<TipoAnimal> TipoAnimais { get; set; }
        public virtual DbSet<TipoContato> TipoContatos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);
            // Realiza o DROP no banco de dados caso o modelo sofra alterações.
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
            base.OnModelCreating(modelBuilder);
        }
    }
}