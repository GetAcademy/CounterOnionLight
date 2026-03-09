using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CounterOnionLight.Infrastructure.Infrastructure.Entities;

public partial class CounterDbContext : DbContext
{
    public CounterDbContext()
    {
    }

    public CounterDbContext(DbContextOptions<CounterDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<CounterHistory> CounterHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=Counter;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Counters__3214EC07303778C6");

            entity.Property(e => e.LastUpdatedBy).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<CounterHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CounterH__3214EC07BE825D44");

            entity.ToTable("CounterHistory");

            entity.Property(e => e.UpdatedBy).HasMaxLength(100);

            entity.HasOne(d => d.Counter).WithMany(p => p.CounterHistories)
                .HasForeignKey(d => d.CounterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CounterHi__Count__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
