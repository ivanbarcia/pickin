using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pickin.Models;

namespace Pickin.Pages.Company
{
    public class CartModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public CartModel(
            Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Producto> Productos { get; set; }

        public async Task OnGetAsync(string company)
        {
            var empresa = await _context.Empresa.FirstOrDefaultAsync(x => x.Codigo == company);

            if (empresa != null)
            {
                Productos = await _context.Producto
                    .Include(i => i.Empresa)
                    .Where(x => x.EmpresaId == empresa.Id)
                    .ToListAsync();
            }
        }
    }
}