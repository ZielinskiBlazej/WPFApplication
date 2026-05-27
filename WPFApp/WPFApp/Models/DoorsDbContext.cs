using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WPFApp.Models;

public partial class DoorsDbContext : DbContext
{
    // To check -> maybe I should put connection string in PATH???
    private readonly string _connectionString = "Server=BLASIUS2086\\SQLEXPRESS;Initial Catalog=DoorsDB;Integrated Security=True;User ID=sa;Password=[password];TrustServerCertificate=true";

    public DoorsDbContext()
    {
    }

    public DoorsDbContext(DbContextOptions<DoorsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Door> Doors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Door>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doors__3214EC07C947EE35");

            entity.Property(e => e.Dmodel)
                .HasMaxLength(50)
                .HasColumnName("dmodel");
            entity.Property(e => e.Dname)
                .HasMaxLength(50)
                .HasColumnName("dname");
            entity.Property(e => e.Dprice)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("dprice");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
