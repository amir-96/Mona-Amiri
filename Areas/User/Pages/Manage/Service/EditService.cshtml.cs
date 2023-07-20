using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
using Mona_Amiri.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Xml.Linq;

namespace Mona_Amiri.Areas.User.Pages.Manage.Service
{
  public class EditServiceModel : PageModel
  {
    private readonly ApplicationDbContext _context;
    public EditServiceModel(ApplicationDbContext context)
    {
      _context = context;
    }

    public Mona_Amiri.Models.Service Service { get; set; }
    public async Task<IActionResult> OnGet(string id, string userId)
    {
      if (id != null)
      {
        Service = await _context.Services.FindAsync(int.Parse(id));

        if (Service != null)
        {
          Input = new InputModel
          {
            Id = Service.Id,
            userId = userId,
            Name = Service.Name,
            Price = Service.Price.ToString(),
            DurationHour = Service.Duration.Hours.ToString(),
            DurationMinute = Service.Duration.Minutes.ToString(),
            Description = Service.Description
          };

          return Page();
        }

        return RedirectToPage("/Manage/Service/Index", new { area = "User", userId = userId });
      }

      return RedirectToPage("/Manage/Service/Index", new { area = "User", userId = userId });
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
      [Required]
      public int Id { get; set; }

      [Required]
      public string userId { get; set; }

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

    public async Task<IActionResult> OnPostEditService()
    {
      if (ModelState.IsValid)
      {
        Service = await _context.Services.FindAsync(Input.Id);

        var persianPriceConverter = new PersianPriceConverter();

        Service.Name = Input.Name;
        Service.Description = Input.Description;
        Service.Duration = new TimeSpan(int.Parse(Input.DurationHour), int.Parse(Input.DurationMinute), 0);
        Service.Price = int.Parse(Input.Price);
        Service.PersianPrice = persianPriceConverter.ConvertPriceToPersian(int.Parse(Input.Price));

        await _context.SaveChangesAsync();

        return RedirectToPage("/Manage/Service/Index", new { area = "User", userId = Input.userId });
      }

      ModelState.AddModelError(string.Empty, "ویرایش سرویس ناموفق بود");

      return Page();
    }
  }
}
