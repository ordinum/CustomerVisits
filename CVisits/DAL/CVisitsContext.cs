using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using CVisits.Models;

namespace CVisits.DAL
{
    public partial class CVisitsContext : DbContext
    {

        public DbSet<Accion> Accion { get; set; }
        public DbSet<AccionEstado> AccionEstado { get; set; }
        public DbSet<AccionResponsable> AccionResponsable { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ClienteContacto> ClienteContacto { get; set; }
        public DbSet<ClienteFaena> ClienteFaena { get; set; }
        public DbSet<EstadoVisita> EstadoVisita { get; set; }
        public DbSet<LineaProducto> LineaProducto { get; set; }
        public DbSet<MaquinaEquipo> MaquinaEquipo { get; set; }
        public DbSet<Pais> Pais { get; set; }        
        public DbSet<TipoVisita> TipoVisita { get; set; }
        public DbSet<Visita> Visita { get; set; }               
        public DbSet<UserProfile> UserProfiles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Evitar pluralización...
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Reporte>()
            //    .HasRequired(f => f.Cliente)
            //    .WithRequiredDependent()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Reporte>()
            //    .HasRequired(f => f.Visita)
            //    .WithRequiredDependent()
            //    .WillCascadeOnDelete(false);


            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            ////Muchos-a-Muchos
            //modelBuilder.Entity<SacSistemas>()
            //    .HasMany(c => c.SacSolicitudAccion).WithMany(i => i.SacSistemas)
            //    .Map(t => t.MapLeftKey("SacSistemasID")
            //        .MapRightKey("SacSolicitudAccionID")
            //        .ToTable("SacSistemasSacSolicitudAccion"));

        }


    }
}