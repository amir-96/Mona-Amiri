using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Data;
using Mona_Amiri.Models;

namespace Mona_Amiri.Services
{
  public class SetAppointmentToDb
  {
    private readonly ApplicationDbContext _context;

    public SetAppointmentToDb(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<bool> SetAppointment(string name, string phone, string beautifierRadio, string serviceRadio, int timeRadio)
    {
      try
      {
        var AppointmentMakeupArtist = await _context.MakeupArtists
            .Include(ma => ma.Services)
            .Include(ma => ma.TimeSlots)
            .SingleOrDefaultAsync(ma => ma.Name == beautifierRadio);

        var AppointmentService = AppointmentMakeupArtist.Services.SingleOrDefault(s => s.Name == serviceRadio);

        var AppointmentTimeSlot = AppointmentMakeupArtist.TimeSlots.SingleOrDefault(t => t.Id == timeRadio);

        var appointment = new Appointment
        {
          Name = name,
          PhoneNumber = phone,
          MakeupArtist = AppointmentMakeupArtist,
          Service = AppointmentService,
          //StartTime = AppointmentTimeSlot.StartTime,
          //EndTime = AppointmentTimeSlot.EndTime
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return true;
      }
      catch (DbUpdateException ex)
      {
        // Log the error message
        Console.WriteLine("An error occurred while saving changes to the database:");
        Console.WriteLine(ex.InnerException.Message);
        return false;
      }
    }
  }
}
