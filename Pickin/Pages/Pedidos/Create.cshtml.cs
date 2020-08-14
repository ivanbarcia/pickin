using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pickin.Data;
using Pickin.Models;

namespace Pickin.Pages.Pedidos
{
    public class CreateModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public CreateModel(Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pedido Pedido { get; set; }
        
        [BindProperty]
        public IList<Producto> Productos { get; set; }

        public IActionResult OnGet()
        {
            Productos = _context.Producto.ToList();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Pedido.Total = Convert.ToDecimal(Pedido.Total.ToString().Replace(",", ""));

            if (Pedido.Total <= 0)
                ModelState.AddModelError("Pedido.Total", "Debe ingresar un valor mayor a 0");

            var _empresaId = Convert.ToInt32(User.FindFirst("EmpresaId").Value);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Pedido.UsuarioAlta = User.Identity.Name;

            _context.Pedido.Add(Pedido);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
