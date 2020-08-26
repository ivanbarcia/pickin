using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Pickin.Models;

namespace Pickin.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Pickin.Data.ApplicationDbContext _context;

        public ConfirmEmailModel(
            UserManager<ApplicationUser> userManager, 
            Pickin.Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        [BindProperty]
        public Empresa Empresa { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";

            Empresa = _context.Empresa.FindAsync(user.EmpresaId).Result;

            //if (result.Succeeded)
            //{
            //    return RedirectToPage("/Account/ResetPassword", new { userId, code });
            //}

            return Page();
        }
    }
}
