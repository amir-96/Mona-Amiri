namespace Mona_Amiri.Services
{
  public class PersianCalendarServices
  {
    public string GetPersianDayOfWeek (DayOfWeek dayOfWeek)
    {
      switch (dayOfWeek)
      {
        case DayOfWeek.Saturday:
          return "شنبه";
        case DayOfWeek.Sunday:
          return "یکشنبه";
        case DayOfWeek.Monday:
          return "دوشنبه";
        case DayOfWeek.Tuesday:
          return "سه‌شنبه";
        case DayOfWeek.Wednesday:
          return "چهارشنبه";
        case DayOfWeek.Thursday:
          return "پنجشنبه";
        case DayOfWeek.Friday:
          return "جمعه";
        default:
          return "";
      }
    }
  }
}
