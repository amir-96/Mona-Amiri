using Microsoft.AspNetCore.Mvc;

namespace Mona_Amiri.Services
{
  public class SmsSender
  {
    public bool OnPostSendSms()
    {
      try
      {
        var sender = "1000596446";
        var receptor = "09163097345";
        var message = "مونا امیری\r\nمشخصات رزرو\r\nروز انتخاب شده: %token\r\nشماره ی پیگیری: %token";
        var api = new Kavenegar.KavenegarApi("6E344939344155583634595532746758426D6D75576279316E76486C66384D63482F705159774C4C394A383D");
        api.VerifyLookup(sender, receptor, message);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}
