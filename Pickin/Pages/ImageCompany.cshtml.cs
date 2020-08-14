using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pickin.Data;
using Pickin.Models;

namespace Pickin.Pages
{
    public class ImageCompanyModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public ImageCompanyModel(Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.FindFirst("EmpresaId") != null)
            {
                // to get the user details to load user Image    
                var company = await _context.Empresa.FindAsync(Convert.ToInt32(User.FindFirst("EmpresaId").Value));

                return new FileContentResult(company.Image, "image/jpeg");
            }

            return new FileContentResult(null, "image/jpeg");
        }
    }
}
