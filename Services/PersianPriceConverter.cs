using System.Globalization;

namespace Mona_Amiri.Services
{
  public class PersianPriceConverter
  {
    public string ConvertPriceToPersian(int price)
    {
      var persianCulture = new CultureInfo("fa-IR");
      return price.ToString("N0", persianCulture);
    }
  }
}