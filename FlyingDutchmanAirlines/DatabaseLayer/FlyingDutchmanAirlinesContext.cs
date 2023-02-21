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

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CA214150;Initial Catalog=FlyingDutchmanAirlines;User ID=cadiaz;Password=130284;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airport>(entity =>
        {
            entity.ToTable("Airport");

            entity.Property(e => e.AirportId)
                .ValueGeneratedNever()
                .HasColumnName("AirportID");
            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Iata)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("IATA");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CustomerBooking");

            entity.HasOne(d => d.FlightNumberNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.FlightNumber)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_FlightNumberBooking");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightNumber);

            entity.ToTable("Flight");

            entity.Property(e => e.FlightNumber).ValueGeneratedNever();

            entity.HasOne(d => d.DestinationNavigation).WithMany(p => p.FlightDestinationNavigations)
                .HasForeignKey(d => d.Destination)
                .HasConstraintName("FK_DestinationFlight");

            entity.HasOne(d => d.OriginNavigation).WithMany(p => p.FlightOriginNavigations)
                .HasForeignKey(d => d.Origin)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_OriginFlight");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
