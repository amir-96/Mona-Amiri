using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mona_Amiri.Models
{
  public class TimeSlot
  {
    public int Id { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public DateTime StartTime { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public DateTime EndTime { get; set; }
    [NotMapped]
    public string ShamsiDate { get; set; }
    [NotMapped]
    public string PersianDayOfWeek { get; set; }
  }
}
