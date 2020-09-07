using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Pickin.Pages
{
    public class ImageCompanyModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public ImageCompanyModel(Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return new FileContentResult(null, "image/jpeg");
            }

            var empresa = _context.Empresa.Find(id);

            return new FileContentResult(empresa.Image, "image/jpeg");
        }
    }
}
