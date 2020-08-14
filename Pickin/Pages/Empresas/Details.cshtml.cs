using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pickin.Data;
using Pickin.Models;

namespace Pickin.Pages.Empresas
{
    public class DetailsModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public DetailsModel(Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Empresa Empresa { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empresa = await _context.Empresa.FirstOrDefaultAsync(m => m.Id == id);

            if (Empresa == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
