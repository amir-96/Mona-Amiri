using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
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

    public IList<Mona_Amiri.Models.Service> ServiceList { get; set; }
    public async Task<IActionResult> OnGet(string userId = null)
    {
      if (userId == null)
      {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        userId = claim.Value;
      }

      var MakeupArtist = await _context.MakeupArtists.Include(m => m.Services).FirstOrDefaultAsync(m => m.Id == userId);

      ServiceList = MakeupArtist.Services.ToList();

      return Page();
    }
  }
}
