using System;
using System.Collections.Generic;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.DatabaseLayer;

public partial class FlyingDutchmanAirlinesContext : DbContext
{
    public FlyingDutchmanAirlinesContext()
    {
    }

    public FlyingDutchmanAirlinesContext(DbContextOptions<FlyingDutchmanAirlinesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airport> Airport { get; set; }

    public virtual DbSet<Booking> Booking { get; set; }

    public virtual DbSet<Customer> Customer { get; set; }

    public virtual DbSet<Flight> Flight { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CA214150;Initial Catalog=FlyingDutchmanAirlines;User ID=cadiaz;Password=130284;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airport>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Airport");

            entity.Property(e => e.AirportId).HasColumnName("AirportID");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Iata)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("IATA");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Flight");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
