using Microsoft.AspNetCore.Identity;

namespace Mona_Amiri.Services
{
  public class CustomErrorDescriber : IdentityErrorDescriber
  {
    public override IdentityError DuplicateUserName(string userName)
    {
      return new IdentityError
      {
        Code = nameof(DuplicateUserName),
        Description = $"نام کاربری '{userName}' قبلاً استفاده شده است"
      };
    }

    public override IdentityError DuplicateEmail(string email)
    {
      return new IdentityError
      {
        Code = nameof(DuplicateEmail),
        Description = $"ایمیل '{email}' قبلاً استفاده شده است"
      };
    }

    public override IdentityError InvalidUserName(string userName)
    {
      return new IdentityError
      {
        Code = nameof(InvalidUserName),
        Description = $"نام کاربری یا رمز عبور نامعتبر است"
      };
    }

    public override IdentityError PasswordMismatch()
    {
      return new IdentityError
      {
        Code = nameof(PasswordMismatch),
        Description = "رمز عبور وارد شده نادرست است. لطفاً دوباره تلاش کنید."
      };
    }
  }
}
