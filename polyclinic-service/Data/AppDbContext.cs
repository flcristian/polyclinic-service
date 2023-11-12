﻿using Microsoft.EntityFrameworkCore;
using polyclinic_service.Appointments.Models;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.Users.Models;

namespace polyclinic_service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<Appointment> Appointments { get; set; }
    
    public virtual DbSet<UserAppointment> UserAppointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAppointment>()
            .HasOne(userAppointment => userAppointment.Patient)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserAppointment>()
            .HasOne(userAppointment => userAppointment.Doctor)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserAppointment>()
            .HasOne(userAppointment => userAppointment.Appointment)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}