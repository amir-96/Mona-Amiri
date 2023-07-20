using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Mona_Amiri.Pages.Reservation
{
  public class IndexModel : PageModel
  {
    private readonly ApplicationDbContext _context;
    public IndexModel(ApplicationDbContext context)
    {
      _context = context;
    }

    public List<MakeupArtist> MakeupArtistList;
    public async Task<IActionResult> OnGet()
    {
      try
      {
        MakeupArtistList = await _context.MakeupArtists
                                  .Include(m => m.Services)
                                  .Where(m => m.Services.Count > 0)
                                  .ToListAsync();

        if (MakeupArtistList.Count == 0)
        {
          return RedirectToPage("/Index");
        }

        return Page();
      }
      catch (Exception ex)
      {
        return RedirectToPage("/Index");
      }
    }

    [BindProperty]
    public AppointmentGetDto Input { get; set; }
    public class AppointmentGetDto
    {
      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(50, ErrorMessage = "{0} باید حداقل 2 کاراکتر باشد", MinimumLength = 2)]
      [Display(Name = "نام")]
      public string Name { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [RegularExpression(@"^09\d{9}$", ErrorMessage = "{0} نامعتبر است")]
      [DataType(DataType.PhoneNumber)]
      [Display(Name = "شماره ی تماس")]
      public string PhoneNumber { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [Display(Name = "نام آرایشگر")]
      public string MakeupArtistId { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [Display(Name = "نوع سرویس")]
      public int ServiceId { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [Display(Name = "تاریخ انتخابی")]
      public DateTime DayDate { get; set; }
    }

    public IActionResult OnPostSendSms()
    {
      if (ModelState.IsValid)
      {
        return RedirectToPage("/Reservation/Receipt", new { Name = Input.Name, PhoneNumber = Input.PhoneNumber, MakeupArtistId = Input.MakeupArtistId, ServiceId = Input.ServiceId, DayDate = Input.DayDate });
      }
      return RedirectToPage("/Reservation/Index");
      //try
      //{
      //  var sender = "1000596446";
      //  var receptor = "09163097345";
      //  var token1 = "test";
      //  var token2 = "test2";
      //  var message = "monaamiri\r\nمشخصات رزرو\r\nروز انتخاب شده: %token\r\nشماره ی پیگیری: %token2\r\nتست واحد فنی";
      //  var api = new Kavenegar.KavenegarApi("6E344939344155583634595532746758426D6D75576279316E76486C66384D63482F705159774C4C394A383D");
      //  api.VerifyLookup(sender, receptor, message);
      //  TempData["Test"] = "ارسال شد";
      //  return RedirectToPage("/Reservation/Index");
      //}
      //catch (Exception ex)
      //{
      //  TempData["Test"] = ex.Message;
      //  return RedirectToPage("/Reservation/Index");
      //}
    }
  }
}
