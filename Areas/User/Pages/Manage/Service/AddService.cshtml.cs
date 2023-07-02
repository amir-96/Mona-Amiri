using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
using Mona_Amiri.Services;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;
using System.Xml.Linq;

namespace Mona_Amiri.Areas.User.Pages.Manage.Service
{
  public class AddServiceModel : PageModel
  {
    private readonly ApplicationDbContext _context;
    public AddServiceModel(ApplicationDbContext context)
    {
      _context = context;
    }

    public void OnGet()
    {
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(50, ErrorMessage = "{0} باید حداقل 2 کاراکتر باشد", MinimumLength = 2)]
      [Display(Name = "نام سرویس")]
      public string Name { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(80, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
      [Display(Name = "توضیحات سرویس")]
      public string Description { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [RegularExpression(@"^(0?[0-9]|1[0-9]|2[0-4])$", ErrorMessage = "{0} باید تنها شامل اعداد از 0 تا 24 باشد")]
      [Display(Name = "زمان مورد نیاز (ساعت)")]
      public string DurationHour { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [RegularExpression(@"^([1-9]|[1-5][0-9]|60)$", ErrorMessage = "{0} باید تنها شامل اعداد از 1 تا 60 باشد")]
      [Display(Name = "زمان مورد نیاز (دقیقه)")]
      public string DurationMinute { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [RegularExpression(@"^\d+$", ErrorMessage = "{0} باید تنها شامل اعداد باشد")]
      [Display(Name = "هزینه ی سرویس")]
      public string Price { get; set; }
    }

    public async Task<IActionResult> OnPostAddService()
    {
      if (ModelState.IsValid)
      {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        var userId = claim.Value;

        var makeupArtist = await _context.MakeupArtists.Include(m => m.Services).FirstOrDefaultAsync(m => m.Id == userId);

        var persianPriceConverter = new PersianPriceConverter();

        var service = new Mona_Amiri.Models.Service
        {
          Name = Input.Name,
          Description = Input.Description,
          Duration = new TimeSpan(int.Parse(Input.DurationHour), int.Parse(Input.DurationMinute), 0),
          Price = int.Parse(Input.Price),
          PersianPrice = persianPriceConverter.ConvertPriceToPersian(int.Parse(Input.Price)),
          CreatedDate = DateTime.UtcNow
        };

        makeupArtist.Services.Add(service);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Manage/Service/Index", new { area = "User" });
      }

      ModelState.AddModelError(string.Empty, "افزودن سرویس ناموفق بود");

      return Page();
    }
  }
}
