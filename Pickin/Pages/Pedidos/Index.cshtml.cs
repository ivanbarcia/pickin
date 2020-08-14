using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pickin.Data;
using Pickin.Models;

namespace Pickin.Pages.Pedidos
{
    public class IndexModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public IndexModel(Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Pedido> Pedidos { get;set; }

        public async Task OnGetAsync()
        {
            var _empresaId = Convert.ToInt32(User.FindFirst("EmpresaId").Value);

            Pedidos = await _context.Pedido
                .Where(x => x.Productos.FirstOrDefault().Producto.EmpresaId == _empresaId)
                .ToListAsync();
        }
    }
}
