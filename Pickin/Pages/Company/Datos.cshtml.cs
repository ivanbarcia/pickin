using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pickin.Data;
using Pickin.Models;

namespace Pickin.Pages.Company
{
    public class DatosModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public DatosModel(Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Nombre { get; set; }

            [Required]
            public string Apellido { get; set; }

            [Required]
            public string Direccion { get; set; }

            [Required]
            public string DireccionNro { get; set; }

            public string Piso { get; set; }

            public string Depto { get; set; }

            public string Entrecalles { get; set; }
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToPage("./Cart");
        }
    }
}
