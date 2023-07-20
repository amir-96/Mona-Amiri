using System.ComponentModel.DataAnnotations;

namespace Mona_Amiri.Models
{
  public class WorkTime
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "فیلد را پر کنید")]
    public int StartHour { get; set; } = 0;

    [Required(ErrorMessage = "فیلد را پر کنید")]
    public int StartMinute { get; set; } = 0;

    [Required(ErrorMessage = "فیلد را پر کنید")]
    public int EndHour { get; set; } = 0;

    [Required(ErrorMessage = "فیلد را پر کنید")]
    public int EndMinute { get; set; } = 0;

    [Required(ErrorMessage = "فیلد را پر کنید")]
    [Range(1, 7, ErrorMessage = "مقدار باید بین 1 و 7 باشد")]
    public List<DayOfWeek> FreeDays { get; set; } = new List<DayOfWeek>();
  }
}
