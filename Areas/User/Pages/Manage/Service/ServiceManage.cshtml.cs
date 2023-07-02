using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
using Mona_Amiri.Services;
using System.Data;

namespace Mona_Amiri.Areas.User.Pages.Manage.Service
{
  [Authorize(Roles = SD.AdminEndUser + ", " + SD.CashierEndUser)]
  public class ServiceManageModel : PageModel
  {
    private readonly ApplicationDbContext _context;
    public ServiceManageModel(ApplicationDbContext context)
    {
      _context = context;
    }

    public IList<MakeupArtist> MakeupArtistList;
    public async Task OnGet()
    {
      MakeupArtistList = await _context.MakeupArtists.Include(m => m.Services).ToListAsync();
    }
  }
}
