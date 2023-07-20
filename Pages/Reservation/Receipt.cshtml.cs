using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Mona_Amiri.Pages.Reservation
{
  public class ReceiptModel : PageModel
  {
    private readonly ApplicationDbContext _context;
    public ReceiptModel(ApplicationDbContext context)
    {
      _context = context;
    }

    public AppointmentAddDto Information { get; set; }
    public class AppointmentAddDto
    {
      [Required]
      public string Name { get; set; }

      [Required]
      public string PhoneNumber { get; set; }

      [Required]
      public MakeupArtist ReservedMakeupArtist { get; set; }

      [Required]
      public Service ReservedService { get; set; }

      [Required]
      public DateTime DayDate { get; set; }
    }
    public MakeupArtist ReservedMakeupArtistGet { get; set; }
    public Service ReservedServiceGet { get; set; }
    public async Task<IActionResult> OnGet(string Name, string PhoneNumber, string MakeupArtistId, int ServiceId, DateTime DayDate)
    {
      if (Name == null || PhoneNumber == null || MakeupArtistId == null || ServiceId == null || DayDate == null)
      {
        return RedirectToPage("/Reservation/Index");
      }

      ReservedMakeupArtistGet = await _context.MakeupArtists.SingleOrDefaultAsync(m => m.Id == MakeupArtistId);

      ReservedServiceGet = await _context.Services.SingleOrDefaultAsync(s => s.Id == ServiceId);

      if (ReservedMakeupArtistGet == null || ReservedServiceGet == null)
      {
        return RedirectToPage("/Reservation/Index");
      }

      Information = new AppointmentAddDto()
      {
        Name = Name,
        PhoneNumber = PhoneNumber,
        ReservedMakeupArtist = ReservedMakeupArtistGet,
        ReservedService = ReservedServiceGet,
        DayDate = DayDate
      };

      return Page();
    }
  }
}
