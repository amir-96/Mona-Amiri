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
using System.Xml.Linq;

namespace Mona_Amiri.Areas.User.Pages.Manage
{
  [Authorize(Roles = SD.AdminEndUser + ", " + SD.CashierEndUser)]
  public class EditUserModel : PageModel
  {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public EditUserModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

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
      public string Name
      { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(80, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
      [Display(Name = "توضیحات")]
      public string Description { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(120, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
      [Display(Name = "آدرس")]
      public string Adress { get; set; }

      [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
      [Display(Name = "نوع کاربر")]
      public string Role { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(string? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var MakeupArtist = await _context.MakeupArtists.FindAsync(id);

      var roles = await _userManager.GetRolesAsync(MakeupArtist);

      var Role = roles.FirstOrDefault();

      if (MakeupArtist == null)
      {
        return NotFound();
      }

      Input = new InputModel
      {
        Id = MakeupArtist.Id,
        PhoneNumber = MakeupArtist.PhoneNumber,
        Name = MakeupArtist.Name,
        Description = MakeupArtist.Description,
        Adress = MakeupArtist.Adress,
        Role = Role,
      };

      return Page();
    }

    public async Task<IActionResult> OnPostEditUser()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      var user = await _context.MakeupArtists.FindAsync(Input.Id);

      if (user == null)
      {
        return NotFound();
      }

      user.PhoneNumber = Input.PhoneNumber;
      user.Name = Input.Name;
      user.Description = Input.Description;
      user.Adress = Input.Adress;

      var userRoles = await _userManager.GetRolesAsync(user);
      var Role = userRoles.FirstOrDefault();

      if (Role != SD.AdminEndUser)
      {
        var result = await _userManager.RemoveFromRolesAsync(user, userRoles);

        if (result.Succeeded)
        {
          await _userManager.AddToRoleAsync(user, Input.Role);
        }
      }

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        return NotFound();
      }

      return RedirectToPage("/Manage/UserManagement", new { area = "User" });
    }
  }
}