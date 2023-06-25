using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
using Mona_Amiri.Services;
using System.Globalization;
using System.Linq;

namespace Mona_Amiri.Pages
{
  public class IndexModel : PageModel
  {
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
      _context = context;
    }

    public List<MakeupArtist> MakeupArtists { get; set; }

    public async Task<IActionResult> OnGet()
    {
      if (!_context.MakeupArtists.Any())
      {
        var firstDataHandler = new FirstDataHandler(_context);

        firstDataHandler.AddFirstData();
      }

      MakeupArtists = await _context.MakeupArtists.Where(a => a.Services.Count > 0 && a.TimeSlots.Count > 0)
          .Include(a => a.Services)
          .Include(a => a.TimeSlots)
          .ToListAsync();

      var persianCalendar = new PersianCalendar();
      var persianCalendarServices = new PersianCalendarServices();
      var PersianPriceConverter = new PersianPriceConverter();

      foreach (var artist in MakeupArtists)
      {
        foreach (var SingleService in artist.Services)
        {
          var price = Convert.ToInt32(SingleService.Price);

          SingleService.PersianPrice = PersianPriceConverter.ConvertPriceToPersian(price);
        }

        foreach (var timeSlot in artist.TimeSlots)
        {
          var date = timeSlot.StartTime;

          timeSlot.ShamsiDate = $"{persianCalendar.GetYear(date)}/{persianCalendar.GetMonth(date)}/{persianCalendar.GetDayOfMonth(date)}";
          timeSlot.PersianDayOfWeek = persianCalendarServices.GetPersianDayOfWeek(date.DayOfWeek);
        }
      }

      return Page();
    }

    public async Task<IActionResult> OnPostAddAppointmentHandler(string name, string phone, string beautifierRadio, string serviceRadio, string timeRadio)
    {
      if (!ModelState.IsValid)
      {
        ModelState.AddModelError("", "اطلاعات معتبر نیست");
        return Page();
      }

      if (name != null && phone != null && beautifierRadio != null && serviceRadio != null && timeRadio != null)
      {
        var newTime = int.Parse(timeRadio);

        var setToDbService = new SetAppointmentToDb(_context);

        await setToDbService.SetAppointment(name, phone, beautifierRadio, serviceRadio, newTime);
      }

      return RedirectToPage(nameof(Index));
    }
  }
}