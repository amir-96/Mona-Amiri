using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mona_Amiri.Models
{
  public class MakeupArtist : IdentityUser
  {
    public int Id { get; set; }
    [Required(ErrorMessage = "فیلد را پر کنید")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "حداقل 2 کاراکتر را وارد کنید")]
    public string Name { get; set; }
    [MaxLength(80)]
    public string Description { get; set; } 
    public string Adress { get; set; }
    public List<Service> Services { get; set; }
    public List<TimeSlot> TimeSlots { get; set; }
  }
}
