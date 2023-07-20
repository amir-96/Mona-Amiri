using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
using Mona_Amiri.Services;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;
using System.Xml.Linq;

namespace Mona_Amiri.Pages.User
{
  [Authorize]
  public class IndexModel : PageModel
  {
    private readonly ApplicationDbContext _context;
    public IndexModel(ApplicationDbContext context)
    {
      _context = context;
    }

    public MakeupArtist MakeupArtist { get; set; }
    public async Task<IActionResult> OnGet(string userId = null)
    {
      if (userId == null)
      {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        userId = claim.Value;
      }

      MakeupArtist = await _context.MakeupArtists
                      .Include(m => m.Services).Include(m => m.TimeSlots).FirstOrDefaultAsync(m => m.Id == userId);

      Input = new InputModel
      {
        Id = MakeupArtist.Id,
        PhoneNumber = MakeupArtist.PhoneNumber,
        Name = MakeupArtist.Name,
        Description = MakeupArtist.Description,
        Adress = MakeupArtist.Adress,
      };

      return Page();
    }

    #region Edit

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
      [Required]
      public string Id { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [RegularExpression(@"^09\d{9}$", ErrorMessage = "{0} نامعتبر است")]
      [DataType(DataType.PhoneNumber)]
      [Display(Name = "شماره ی همراه")]
      public string PhoneNumber { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(50, ErrorMessage = "{0} باید حداقل 2 کاراکتر باشد", MinimumLength = 2)]
      [Display(Name = "نام")]
      public string Name { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(80, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
      [Display(Name = "توضیحات")]
      public string Description { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(120, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
      [Display(Name = "آدرس")]
      public string Adress { get; set; }
    }

    public async Task<IActionResult> OnPostEditUser()
    {
      var user = await _context.MakeupArtists.FindAsync(Input.Id);

      if (user == null)
      {
        return NotFound();
      }

      user.PhoneNumber = Input.PhoneNumber;
      user.Name = Input.Name;
      user.Description = Input.Description;
      user.Adress = Input.Adress;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        return NotFound();
      }

      return RedirectToPage("/Manage/Index", new { area = "User" });
    }

    #endregion

    #region Image upload

    [BindProperty]
    public IFormFile ImgUpload { get; set; }
    public async Task<IActionResult> OnPostAddImage(string id)
    {
      if (Path.GetExtension(ImgUpload.FileName) != ".jpg" && Path.GetExtension(ImgUpload.FileName) != ".png" && Path.GetExtension(ImgUpload.FileName) != ".jpeg")
      {
        return RedirectToPage("/Manage/Index", new { area = "User" });
      }

      if (ImgUpload.Length > (1 * 1024 * 1024))
      {
        return RedirectToPage("/Manage/Index", new { area = "User" });
      }

      if (id != null)
      {
        try
        {
          MakeupArtist = await _context.MakeupArtists.FindAsync(id);

          if (ImgUpload != null && MakeupArtist != null)
          {
            MakeupArtist.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUpload.FileName);
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ProfilePhoto", MakeupArtist.ImageName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
              ImgUpload.CopyTo(fileStream);
            }
          }

          await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
          TempData["Message"] = ex.Message;
          return RedirectToPage("/Manage/Index", new { area = "User" });
        }
      }

      return RedirectToPage("/Manage/Index", new { area = "User" });
    }

    #endregion
  }
}
