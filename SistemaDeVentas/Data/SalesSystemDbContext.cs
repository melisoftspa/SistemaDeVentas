using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Models;

namespace SistemaDeVentas.Data;

public partial class SalesSystemDbContext : DbContext
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\;Database=SalesSystemDB;Integrated Security=SSPI;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("category");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Text).HasMaxLength(500);
            entity.Property(e => e.Value).HasDefaultValueSql("((-1))");
        });

        modelBuilder.Entity<Decrease>(entity =>
        {
            entity.ToTable("decrease");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.Line)
                .HasMaxLength(4000)
                .HasColumnName("line");
            entity.Property(e => e.Note)
                .HasMaxLength(1000)
                .HasColumnName("note");
            entity.Property(e => e.Tax).HasColumnName("tax");
            entity.Property(e => e.Total).HasColumnName("total");
        });

        modelBuilder.Entity<Detail>(entity =>
        {
            entity.ToTable("detail");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdSale).HasColumnName("id_sale");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(500)
                .HasColumnName("product_name");
            entity.Property(e => e.State)
                .HasDefaultValueSql("((1))")
                .HasColumnName("state");
            entity.Property(e => e.Tax).HasColumnName("tax");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.TotalTax).HasColumnName("total_tax");
        });

        modelBuilder.Entity<Entry>(entity =>
        {
            entity.ToTable("entries");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.NameProduct)
                .HasMaxLength(1000)
                .HasColumnName("name_product");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Total).HasColumnName("total");
        });

        modelBuilder.Entity<Historical>(entity =>
        {
            entity.ToTable("historical");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.After).HasColumnName("after");
            entity.Property(e => e.Before).HasColumnName("before");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("type");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("invoice");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Name)
                .HasMaxLength(1500)
                .HasColumnName("name");
            entity.Property(e => e.NameProvider)
                .HasMaxLength(1500)
                .HasColumnName("name_provider");
            entity.Property(e => e.Notes)
                .HasMaxLength(4000)
                .HasColumnName("notes");
            entity.Property(e => e.PaymentCash).HasColumnName("payment_cash");
            entity.Property(e => e.PaymentCashTotal).HasColumnName("payment_cash_total");
            entity.Property(e => e.PaymentCheck).HasColumnName("payment_check");
            entity.Property(e => e.PaymentCheckDate)
                .HasColumnType("date")
                .HasColumnName("payment_check_date");
            entity.Property(e => e.PaymentCheckNumber)
                .HasMaxLength(500)
                .HasColumnName("payment_check_number");
            entity.Property(e => e.PaymentCheckTotal).HasColumnName("payment_check_total");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.StatePayment).HasColumnName("state_payment");
            entity.Property(e => e.Total).HasColumnName("total");
        });

        modelBuilder.Entity<Pack>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("pack");

            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.BarCode)
                .HasMaxLength(500)
                .HasColumnName("bar_code");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.IdPack).HasColumnName("id_pack");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.Line)
                .HasMaxLength(1500)
                .HasColumnName("line");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.State).HasColumnName("state");
        });

        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.ToTable("parameter");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Module)
                .HasMaxLength(1000)
                .HasColumnName("module");
            entity.Property(e => e.Name)
                .HasMaxLength(1000)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(500)
                .HasColumnName("type");
            entity.Property(e => e.Value)
                .HasMaxLength(4000)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.BarCode)
                .HasMaxLength(500)
                .HasColumnName("bar_code");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Exenta)
                .HasDefaultValueSql("((0))")
                .HasColumnName("exenta");
            entity.Property(e => e.Expiration)
                .HasColumnType("date")
                .HasColumnName("expiration");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.IdPack).HasColumnName("id_pack");
            entity.Property(e => e.IdSubcategory).HasColumnName("id_subcategory");
            entity.Property(e => e.IdTax).HasColumnName("id_tax");
            entity.Property(e => e.IsPack).HasColumnName("isPack");
            entity.Property(e => e.Line)
                .HasMaxLength(1500)
                .HasColumnName("line");
            entity.Property(e => e.Minimum).HasColumnName("minimum");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.Photo)
                .HasColumnType("image")
                .HasColumnName("photo");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.SalePrice).HasColumnName("sale_price");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("sale");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Change).HasColumnName("change");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .HasColumnName("note");
            entity.Property(e => e.PaymentCash).HasColumnName("payment_cash");
            entity.Property(e => e.PaymentOther).HasColumnName("payment_other");
            entity.Property(e => e.State)
                .HasDefaultValueSql("((1))")
                .HasColumnName("state");
            entity.Property(e => e.Tax).HasColumnName("tax");
            entity.Property(e => e.Ticket).HasColumnName("ticket");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_sale_users");
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.ToTable("subcategory");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.State)
                .HasDefaultValueSql("((1))")
                .HasColumnName("state");
            entity.Property(e => e.Text)
                .HasMaxLength(500)
                .HasColumnName("text");
            entity.Property(e => e.Value)
                .HasMaxLength(500)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Tax>(entity =>
        {
            entity.ToTable("tax");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Exenta)
                .HasDefaultValueSql("((0))")
                .HasColumnName("exenta");
            entity.Property(e => e.Text).HasMaxLength(500);
            entity.Property(e => e.Value).HasMaxLength(500);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.InInvoice)
                .HasDefaultValueSql("((0))")
                .HasColumnName("in_invoice");
            entity.Property(e => e.Name)
                .HasMaxLength(1000)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(1000)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(500)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionAdd)
                .HasDefaultValueSql("((0))")
                .HasColumnName("actionAdd");
            entity.Property(e => e.ActionDelete)
                .HasDefaultValueSql("((0))")
                .HasColumnName("actionDelete");
            entity.Property(e => e.ActionEdit)
                .HasDefaultValueSql("((0))")
                .HasColumnName("actionEdit");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.State)
                .HasDefaultValueSql("((0))")
                .HasColumnName("state");
            entity.Property(e => e.Username)
                .HasColumnType("text")
                .HasColumnName("username");
            entity.Property(e => e.View)
                .HasColumnType("text")
                .HasColumnName("view");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
