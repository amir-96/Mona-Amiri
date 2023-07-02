using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
using Mona_Amiri.Services;
using System.Data;
using System.Security.Claims;

namespace Mona_Amiri.Areas.User.Pages.Manage.Service
{
  public class IndexModel : PageModel
  {
    private readonly ApplicationDbContext _context;
    public IndexModel(ApplicationDbContext context)
    {
      _context = context;
    }

    public MakeupArtist MakeupArtist { get; set; }
    public IList<Mona_Amiri.Models.Service> ServiceList { get; set; }
    public async Task<IActionResult> OnGet(string userId = null)
    {
      if (User.IsInRole(SD.EmployerEndUser))
      {
        userId = null;
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        userId = claim.Value;
      }

      if (userId == null)
      {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        userId = claim.Value;
      }

      MakeupArtist = await _context.MakeupArtists.Include(m => m.Services).FirstOrDefaultAsync(m => m.Id == userId);

      ServiceList = MakeupArtist.Services.ToList();

      return Page();
    }

    #region Delete

    public async Task<IActionResult> OnPostDeleteService(string id, string userId)
    {
      var Service = await _context.Services.FindAsync(int.Parse(id));

      if (Service != null)
      {
        _context.Services.Remove(Service);

        await _context.SaveChangesAsync();

        TempData["Message"] = "سرویس با موفقیت حذف شد";

        return RedirectToPage("/Manage/Service/Index", new { area = "User", userId = userId });
      }

      TempData["Message"] = "حذف سرویس ناموفق بود";

      return RedirectToPage("/Manage/Service/Index", new { area = "User", userId = userId });
    }

    #endregion
  }
}
