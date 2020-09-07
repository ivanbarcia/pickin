using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Pickin.Models;

namespace Pickin.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly Pickin.Data.ApplicationDbContext _context;

        public ForgotPasswordModel(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            Pickin.Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public int? EmpresaId { get; set; }

        [BindProperty]
        public byte[] EmpresaImage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Debe ingresar un Email")]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task OnGetAsync(int? id = null)
        {
            if (id.HasValue)
            {
                var empresa = await _context.Empresa.FindAsync(id);
                EmpresaId = id.Value;
                EmpresaImage = empresa.Image;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                if (EmpresaId.HasValue)
                {
                    if (user.EmpresaId != EmpresaId)
                    {
                        // Don't reveal that the user does not exist or is not confirmed
                        return RedirectToPage("./ForgotPasswordConfirmation", new { id = EmpresaId });
                    }
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var userId = user.Id;
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", userId, code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToPage("./ForgotPasswordConfirmation", new { id = EmpresaId });
            }

            if (EmpresaId.HasValue)
            {
                var empresa = await _context.Empresa.FindAsync(EmpresaId);
                EmpresaId = EmpresaId;
                EmpresaImage = empresa.Image;
            }

            return Page();
        }
    }
}
