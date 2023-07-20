using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
using Mona_Amiri.Services;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Mona_Amiri.Pages
{
  public class IndexModel : PageModel
  {
    public void OnGet()
    {
    }
  }
}