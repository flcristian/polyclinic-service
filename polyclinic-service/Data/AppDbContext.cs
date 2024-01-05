using Microsoft.EntityFrameworkCore;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Schedules.Models;
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
    
    public virtual DbSet<Schedule> Schedules { get; set; }
    
    public virtual DbSet<ScheduleSlot> ScheduleSlots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAppointment>()
            .HasOne(userAppointment => userAppointment.User)
            .WithMany(user => user.UserAppointments)
            .HasForeignKey(userAppointment => userAppointment.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserAppointment>()
            .HasOne(userAppointment => userAppointment.Appointment)
            .WithMany(appointment => appointment.UserAppointments)
            .HasForeignKey(userAppointment => userAppointment.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Schedule>()
            .HasOne(s => s.MondaySchedule)
            .WithMany()
            .HasForeignKey(s => s.MondayScheduleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Schedule>()
            .HasOne(s => s.TuesdaySchedule)
            .WithMany()
            .HasForeignKey(s => s.TuesdayScheduleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Schedule>()
            .HasOne(s => s.WednesdaySchedule)
            .WithMany()
            .HasForeignKey(s => s.WednesdayScheduleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Schedule>()
            .HasOne(s => s.ThursdaySchedule)
            .WithMany()
            .HasForeignKey(s => s.ThursdayScheduleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Schedule>()
            .HasOne(s => s.FridaySchedule)
            .WithMany()
            .HasForeignKey(s => s.FridayScheduleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Schedule>()
            .HasOne(schedule => schedule.Doctor)
            .WithOne(user => user.WorkSchedule)
            .HasForeignKey<Schedule>(schedule => schedule.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}