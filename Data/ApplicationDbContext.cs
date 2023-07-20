using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Models;

namespace Mona_Amiri.Data
{
  public class ApplicationDbContext : IdentityDbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<WorkTime> WorkTime { get; set; }
    public DbSet<MakeupArtist> MakeupArtists { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
  }
}