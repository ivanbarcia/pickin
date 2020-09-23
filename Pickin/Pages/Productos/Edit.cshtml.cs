using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pickin.Data;
using Pickin.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Pickin.Pages.Productos
{
    public class EditModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public EditModel(Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Producto Producto { get; set; }

        [BindProperty]
        public IFormFile postedFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Producto = await _context.Producto
                .Include(i => i.Empresa).FirstOrDefaultAsync(m => m.Id == id);

            if (Producto == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (Producto.Precio <= 0)
                ModelState.AddModelError("Producto.Precio", "Debe ingresar un valor mayor a 0");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (postedFile != null && postedFile.Length != 0)
            {
                MemoryStream ms = new MemoryStream();
                postedFile.CopyTo(ms);
                Producto.Image = ms.ToArray();
            }

            Producto.UsuarioModificacion = this.User.Identity.Name;

            _context.Attach(Producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(Producto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
    }
}
