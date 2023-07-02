using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
using Mona_Amiri.Services;
using System.Data;

namespace Mona_Amiri.Areas.User.Pages.Manage
{
  public class UserManagementModel : PageModel
  {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public UserManagementModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    [BindProperty]
    public List<MakeupArtist> MakeupArtistList { get; set; }

    #region Page start

    public async Task OnGet()
    {
      MakeupArtistList = await _context.MakeupArtists.ToListAsync();

      foreach (var artist in MakeupArtistList)
      {
        var user = await _userManager.FindByIdAsync(artist.Id);

        if (user != null)
        {
          var roles = await _userManager.GetRolesAsync(user);
          artist.Role = TranslateRoles(roles);
        }
      }

      MakeupArtistList = MakeupArtistList.OrderBy(artist => RoleOrder(artist.Role)).ToList();
    }

    private string TranslateRoles(IList<string> roles)
    {
      var translatedRoles = new List<string>();

      foreach (var role in roles)
      {
        string translatedRole;

        switch (role)
        {
          case "Admin":
            translatedRole = "مدیر";
            break;
          case "Cashier":
            translatedRole = "منشی";
            break;
          case "Employer":
            translatedRole = "آرایشگر";
            break;
          default:
            translatedRole = role;
            break;
        }

        translatedRoles.Add(translatedRole);
      }

      return translatedRoles[0];
    }

    private int RoleOrder(string role)
    {
      switch (role)
      {
        case "مدیر":
          return 1;
        case "منشی":
          return 2;
        case "آرایشگر":
          return 3;
        default:
          return 4;
      }
    }

    #endregion

    #region Delete

    public async Task<IActionResult> OnPostDeleteUser(string id)
    {
      var user = await _context.MakeupArtists.FindAsync(id);

      if (user != null)
      {
        _context.MakeupArtists.Remove(user);

        await _context.SaveChangesAsync();

        TempData["Message"] = "کاربر با موفقیت حذف شد";

        return RedirectToPage("/Manage/UserManagement", new { area = "User" });
      }

      TempData["Message"] = "حذف کاربر ناموفق بود";

      return RedirectToPage("/Manage/UserManagement", new { area = "User" });
    }

    #endregion
  }
}
