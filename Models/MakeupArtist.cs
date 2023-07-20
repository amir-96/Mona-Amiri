using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mona_Amiri.Models
{
  public class MakeupArtist : IdentityUser
  {
    [Required(ErrorMessage = "فیلد را پر کنید")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "حداقل 2 کاراکتر را وارد کنید")]
    public string Name { get; set; }
    [Required(ErrorMessage = "فیلد را پر کنید")]
    [MaxLength(80)]
    public string Description { get; set; }
    [Required(ErrorMessage = "فیلد را پر کنید")]
    [MaxLength(120)]
    public string Adress { get; set; }
    public string ImageName { get; set; } = "DefaultProfile.png";
    public List<Service> Services { get; set; }
    public List<Appointment> Appointments { get; set; }
    public List<TimeSlot> TimeSlots { get; set; }
    [NotMapped]
    public string Role { get; set; }
  }
}
