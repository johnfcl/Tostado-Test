using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tostao.Domain.Entities;

namespace Tostao.Infraestructure.Context
{
    public class TostaoAppDbContext : DbContext
    {
        public TostaoAppDbContext(DbContextOptions<TostaoAppDbContext> options) : base(options) { }

        public DbSet<Documento> Documentos { get; set; }
        public DbSet<MovimientoDocumento> MovimientosDocumento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Documentos configuration
            modelBuilder.Entity<Documento>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Autor).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Tipo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Estado).IsRequired().HasMaxLength(50);
                entity.Property(e => e.RutaArchivo).HasMaxLength(500);

                entity.HasIndex(e => e.Autor);
                entity.HasIndex(e => e.Tipo);
                entity.HasIndex(e => new { e.Estado, e.FechaRegistro });
            });

            // MovimientosDocumento configuration
            modelBuilder.Entity<MovimientoDocumento>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Usuario).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Accion).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Observaciones).HasMaxLength(500);

                entity.HasIndex(e => e.DocumentoId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

