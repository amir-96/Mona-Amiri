using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mona_Amiri.Models
{
  public class Appointment
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "فیلد را پر کنید")]
    public string Name { get; set; }

    [Required(ErrorMessage = "فیلد را پر کنید")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "فیلد را پر کنید")]
    public string MakeupArtistId { get; set; }

    [Required(ErrorMessage = "فیلد را پر کنید")]
    public MakeupArtist MakeupArtist { get; set; }

    [Required(ErrorMessage = "فیلد را پر کنید")]
    public int ServiceId { get; set; }

    [Required(ErrorMessage = "فیلد را پر کنید")]
    public Service Service { get; set; }

    [Required(ErrorMessage = "فیلد را پر کنید")]
    public DateTime Day { get; set; }
  }
}
