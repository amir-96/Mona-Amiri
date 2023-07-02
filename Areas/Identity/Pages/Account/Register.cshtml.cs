// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Mona_Amiri.Data;
using Mona_Amiri.Models;
using Mona_Amiri.Services;

namespace Mona_Amiri.Areas.Identity.Pages.Account
{
  [Authorize(Roles = SD.AdminEndUser)]
  public class RegisterModel : PageModel
  {
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly IUserEmailStore<IdentityUser> _emailStore;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailSender _emailSender;
    private readonly RoleManager<IdentityRole> _RoleManager;
    private readonly ApplicationDbContext _context;

    public RegisterModel(
        UserManager<IdentityUser> userManager,
        IUserStore<IdentityUser> userStore,
        SignInManager<IdentityUser> signInManager,
        ILogger<RegisterModel> logger,
        IEmailSender emailSender,
        RoleManager<IdentityRole> RoleManager,
        ApplicationDbContext context)
    {
      _userManager = userManager;
      _userStore = userStore;
      _emailStore = GetEmailStore();
      _signInManager = signInManager;
      _logger = logger;
      _emailSender = emailSender;
      _RoleManager = RoleManager;
      _context = context;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }

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
      [Display(Name = "نام شخص")]
      public string Name { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(80, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
      [Display(Name = "توضیحات")]
      public string Description { get; set; }

      [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
      [StringLength(120, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد", MinimumLength = 8)]
      [Display(Name = "آدرس")]
      public string Adress { get; set; }
    }


    public async Task<IActionResult> OnGetAsync(string returnUrl = null)
    {
      //if (_signInManager.IsSignedIn(User))
      //{
      //  return RedirectToPage("/Index");
      //}
      ReturnUrl = returnUrl;
      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      return Page();
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
      returnUrl ??= Url.Content("~/");
      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      if (ModelState.IsValid)
      {
        var user = CreateUser();

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

          await _userManager.AddToRoleAsync(user, SD.EmployerEndUser);

          _logger.LogInformation("User created a new account with password.");

          var userId = await _userManager.GetUserIdAsync(user);
          var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
          code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
          var callbackUrl = Url.Page(
              "/Account/ConfirmEmail",
              pageHandler: null,
              values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
              protocol: Request.Scheme);

          string message = $@"
            <html dir='rtl'>
                <head>
                    <style>
                        .container {{
                          direction: rtl;
                          margin: 0 auto;
                          padding: 20px;
                          border: 1px solid black;
                          border-radius: 12px;
                          width: 80%;
                          text-align: right;
                          font-size: 20px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <p>{user.UserName} عزیز</p>
                        <p>با تشکر از ثبت نام شما در سایت ما, برای فعال سازی اکانت خود روی لینک زیر کلیک کنید:</p>
                        <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' style='display: inline-block; margin: 0 auto; padding: 10px 20px; background-color: #4CAF50; color: #fff; text-decoration: none; border-radius: 4px;'>فعال سازی حساب کاربری</a>
                        <p>اگر برای سرویس ما ثبت نام نکرده اید, این ایمیل را نادیده بگیرید</p>
                        <p>با تشکر از شما. سالن زیبایی مونا امیری</p>
                    </div>
                </body>
            </html>";

          //await _emailSender.SendEmailAsync(Input.Email, "سرویس تست", message);

          if (_userManager.Options.SignIn.RequireConfirmedAccount)
          {
            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
          }
          else
          {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
          }
        }
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }

    private IdentityUser CreateUser()
    {
      try
      {
        var user = new MakeupArtist { UserName = Input.UserName, Email = Input.Email, Name = Input.Name, PhoneNumber = Input.PhoneNumber, Description = Input.Description, Adress = Input.Adress };

        return user;
      }
      catch
      {
        throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
            $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
            $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
      }
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
