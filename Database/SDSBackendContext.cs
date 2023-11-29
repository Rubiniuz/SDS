using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SDS.Models;

namespace SDS.Database;

public partial class SDSBackendContext : DbContext
{
    public SDSBackendContext(DbContextOptions<SDSBackendContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId).HasName("PRIMARY");

            entity.ToTable("article");

            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Price).HasPrecision(12, 4);
            entity.Property(e => e.Sku).HasMaxLength(24);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
