using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pickin.Data;
using Pickin.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Pickin.Pages.Empresas
{
    public class CreateModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public CreateModel(Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Empresa Empresa { get; set; }

        [BindProperty]
        public IFormFile postedFile { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (postedFile != null && postedFile.Length != 0)
            {
                MemoryStream ms = new MemoryStream();
                postedFile.CopyTo(ms);
                Empresa.Image = ms.ToArray();
            }

            _context.Empresa.Add(Empresa);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
