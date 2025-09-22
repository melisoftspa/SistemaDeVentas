using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Infrastructure.Data;

public partial class SalesSystemDbContext : DbContext, ISalesDbContext
{
    public SalesSystemDbContext()
    {
    }

    public SalesSystemDbContext(DbContextOptions<SalesSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Decrease> Decreases { get; set; }
    public virtual DbSet<Detail> Details { get; set; }
    public virtual DbSet<Entry> Entries { get; set; }
    public virtual DbSet<Historical> Historicals { get; set; }
    public virtual DbSet<Invoice> Invoices { get; set; }
    public virtual DbSet<Pack> Packs { get; set; }
    public virtual DbSet<Parameter> Parameters { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    public virtual DbSet<Subcategory> Subcategories { get; set; }
    public virtual DbSet<Tax> Taxes { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Caf> Cafs { get; set; }
    public virtual DbSet<CertificateData> CertificateDatas { get; set; }
    public virtual DbSet<DteLog> DteLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=SalesSystemDB;User Id=sa;Password=;Trust Server Certificate=true;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Keep mappings in the original project or replicate needed ones here
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
