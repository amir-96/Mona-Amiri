using Mona_Amiri.Data;
using Mona_Amiri.Models;

namespace Mona_Amiri.Services
{
  public class FirstDataHandler
  {
    private readonly ApplicationDbContext _context;
    public FirstDataHandler(ApplicationDbContext context) { _context = context; }
    public void AddFirstData()
    {
      // Create a new MakeupArtist object
      var makeupArtist = new MakeupArtist
      {
        Name = "مونا امیری",
        Description = "20 سال سابقه ی کار"
      };

      // Add the MakeupArtist object to the database
      _context.MakeupArtists.Add(makeupArtist);
      _context.SaveChanges();

      // Create a new Service object
      var service = new Service
      {
        Name = "رنگ مو",
        Description = "رنگ مو با بهترین متریال",
        Duration = TimeSpan.FromMinutes(20),
        Price = 200000
      };

      // Add the Service object to the database
      _context.Services.Add(service);
      _context.SaveChanges();

      // Create a new TimeSlot object
      var timeSlot = new TimeSlot
      {
        StartTime = DateTime.Now.ToUniversalTime(),
        EndTime = DateTime.Now.AddHours(1).ToUniversalTime(),
      };
      _context.TimeSlots.Add(timeSlot);
      _context.SaveChanges();


      makeupArtist.Services = new List<Service> { service };
      makeupArtist.TimeSlots = new List<TimeSlot> { timeSlot };
      _context.SaveChanges();
    }
  }
}
