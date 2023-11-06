using Microsoft.EntityFrameworkCore;
using polyclinic_service.Appointments.Model;
using polyclinic_service.Users.Model;

namespace polyclinic_service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<Appointment> Appointments { get; set; }
}