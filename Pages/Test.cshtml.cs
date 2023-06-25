using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mona_Amiri.Data;
using Mona_Amiri.Models;

namespace Mona_Amiri.Pages
{
  public class TestModel : PageModel
  {
    private readonly ApplicationDbContext _context;
    public TestModel(ApplicationDbContext context)
    {
      _context = context;
    }

    public void OnGet()
    {
    }

    [BindProperty]
    public MakeupArtist MakeupArtist { get; set; }

    public IActionResult OnPostAddMakeupArtist(MakeupArtist model)
    {
      if(!ModelState.IsValid)
      {
        ModelState.AddModelError("", "ورودی ها نامعتیر است");
        return RedirectToPage("/Test");
      }

      return RedirectToPage("/Test");
    }
  }
}