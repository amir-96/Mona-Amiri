using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Mona_Amiri.Areas.Identity.Pages.Account;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
using Mona_Amiri.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Mona_Amiri.Areas.User.Pages.Manage
{
  [Authorize(Roles = SD.AdminEndUser)]
  public class AddUserModel : PageModel
  {
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly IUserEmailStore<IdentityUser> _emailStore;
    private readonly ILogger<RegisterModel> _logger;
    private readonly RoleManager<IdentityRole> _RoleManager;
    private readonly ApplicationDbContext _context;

    public AddUserModel(
        UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore,
        SignInManager<IdentityUser> signInManager,
        ILogger<RegisterModel> logger,
        RoleManager<IdentityRole> RoleManager,
        ApplicationDbContext context)
    {
      _userManager = userManager;
      _userStore = userStore;
      _emailStore = GetEmailStore();
      _signInManager = signInManager;
      _logger = logger;
      _RoleManager = RoleManager;
      _context = context;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [EmailAddress(ErrorMessage = "{0} نا معتبر است")]
      [Display(Name = "ایمیل")]
      public string Email { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(50, ErrorMessage = "{0} باید حداقل 2 کاراکتر باشد", MinimumLength = 2)]
      [Display(Name = "نام کاربری")]
      public string UserName { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [RegularExpression(@"^09\d{9}$", ErrorMessage = "{0} نامعتبر است")]
      [DataType(DataType.PhoneNumber)]
      [Display(Name = "شماره ی همراه")]
      public string PhoneNumber { get; set; }

      [Required(ErrorMessage = "پر کردن این فیلد اجباری است")]
      [StringLength(100, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
      [DataType(DataType.Password)]
      [Display(Name = "کلمه ی عبور")]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "تکرار کلمه ی عبور")]
      [Compare("Password", ErrorMessage = "کلمه ی عبور و تکرار آن باهم مطابقت ندارند")]
      public string ConfirmPassword { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(50, ErrorMessage = "{0} باید حداقل 2 کاراکتر باشد", MinimumLength = 2)]
      [Display(Name = "نام")]
      public string FirstName { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(50, ErrorMessage = "{0} باید حداقل 2 کاراکتر باشد", MinimumLength = 2)]
      [Display(Name = "نام خانوادگی")]
      public string LastName { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(80, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
      [Display(Name = "توضیحات")]
      public string Description { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(120, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
      [Display(Name = "آدرس")]
      public string Adress { get; set; }

      [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
      [Display(Name = "نوع کاربر")]
      public string Role { get; set; }
    }


    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAddUser()
    {
      if (ModelState.IsValid)
      {
        var user = new MakeupArtist { UserName = Input.UserName, Email = Input.Email, Name = Input.FirstName + " " + Input.LastName, PhoneNumber = Input.PhoneNumber, Description = Input.Description, Adress = Input.Adress };

        await _userStore.SetUserNameAsync(user, Input.UserName, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
          if (!await _RoleManager.RoleExistsAsync(SD.AdminEndUser))
          {
            await _RoleManager.CreateAsync(new IdentityRole(SD.AdminEndUser));
          }
          if (!await _RoleManager.RoleExistsAsync(SD.CashierEndUser))
          {
            await _RoleManager.CreateAsync(new IdentityRole(SD.CashierEndUser));
          }
          if (!await _RoleManager.RoleExistsAsync(SD.EmployerEndUser))
          {
            await _RoleManager.CreateAsync(new IdentityRole(SD.EmployerEndUser));
          }

          await _userManager.AddToRoleAsync(user, Input.Role);

          return RedirectToPage("/Manage/UserManagement", new { area = "User" });
        }

        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      return Page();
    }

    private IUserEmailStore<IdentityUser> GetEmailStore()
    {
      if (!_userManager.SupportsUserEmail)
      {
        throw new NotSupportedException("The default UI requires a user store with email support.");
      }
      return (IUserEmailStore<IdentityUser>)_userStore;
    }
  }
}
