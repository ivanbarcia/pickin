using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pickin.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public ResetPasswordConfirmationModel(Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int EmpresaId { get; set; }

        [BindProperty]
        public byte[] EmpresaImage { get; set; }

        public async Task OnGetAsync(int? id = null)
        {
            if (id.HasValue)
            {
                var empresa = await _context.Empresa.FindAsync(id.Value);
                EmpresaId = id.Value;
                EmpresaImage = empresa.Image;
            }
        }
    }
}
