using System.ComponentModel.DataAnnotations;

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
    public int ArtistId { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public MakeupArtist Artist { get; set; }

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
