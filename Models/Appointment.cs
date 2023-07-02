using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mona_Amiri.Models
{
  public class Appointment
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "فیلد را پر کتید")]
    public string Name { get; set; }

    [Required(ErrorMessage = "فیلد را پر کتید")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "فیلد را پر کتید")]
    public int MakeupArtistId { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public MakeupArtist MakeupArtist { get; set; }

    [Required(ErrorMessage = "فیلد را پر کتید")]
    public int ServiceId { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public Service Service { get; set; }

    [Required(ErrorMessage = "فیلد را پر کتید")]
    public int TimeslotId { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public TimeSlot Timeslot { get; set; }

  }
}
