using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mona_Amiri.Models
{
  public class Service
  {
    public int Id { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public string Name { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public string Description { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public TimeSpan Duration { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "فیلد را پر کتید")]
    public string PersianPrice { get; set; }
  }
}
