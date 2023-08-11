using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PruebaTec.Models;

public partial class PtitGroupContext : DbContext
{
    public PtitGroupContext()
    {
    }

    public PtitGroupContext(DbContextOptions<PtitGroupContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ELE-OS\\SQLEXPRESS;Database=PTItGroup; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3213E83F40860362");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClienteValidacion).HasColumnName("cliente_validacion");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.Correo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.PerfilImg).HasColumnName("perfilImg");
            entity.Property(e => e.Usuario)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
