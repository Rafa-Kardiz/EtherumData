using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace EtherumData.Models;

public partial class EthereumDataContext : DbContext
{
    public EthereumDataContext()
    {
    }

    public EthereumDataContext(DbContextOptions<EthereumDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Transaction> Transactions { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("transactions");

            entity.Property(e => e.Id)
                .HasMaxLength(66)
                .HasColumnName("id");
            entity.Property(e => e.BlockNumber)
                .HasColumnType("bigint(20)")
                .HasColumnName("blockNumber");
            entity.Property(e => e.FromAddress)
                .HasMaxLength(42)
                .HasColumnName("fromAddress");
            entity.Property(e => e.GasUsed)
                .HasColumnType("bigint(20)")
                .HasColumnName("gasUsed");
            entity.Property(e => e.ToAdress)
                .HasMaxLength(42)
                .HasColumnName("toAdress");
            entity.Property(e => e.Value)
                .HasColumnType("bigint(20)")
                .HasColumnName("value");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
