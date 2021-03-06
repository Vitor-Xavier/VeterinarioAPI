﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VeterinarioAPI.Controllers;
using VeterinarioAPI.Models;

namespace VeterinarioAPI.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=VETERINARIODB") { } //"Veterinario_DB"VETERINARIODB"

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

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((EntityBase)entity.Entity).CreatedAt = DateTime.UtcNow;
                    ((EntityBase)entity.Entity).UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    this.Entry(((EntityBase)entity.Entity)).Property(x => x.CreatedAt).IsModified = false;
                    ((EntityBase)entity.Entity).UpdatedAt = DateTime.UtcNow;
                }
                    
            }
            return base.SaveChanges();
        }
    }
}