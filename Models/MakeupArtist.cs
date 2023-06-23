using System.ComponentModel.DataAnnotations;

namespace Mona_Amiri.Models
{
  public class MakeupArtist
  {
    public int Id { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "حداقل 2 کاراکتر را وارد کنید")]
    public string Name { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    [MaxLength(80)]
    public string Description { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public List<Service> Services { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public List<TimeSlot> TimeSlots { get; set; }
  }
}
