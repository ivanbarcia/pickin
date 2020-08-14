using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pickin.Data;
using Pickin.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;

namespace Pickin.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public IndexModel(
            Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Producto> Productos { get; set; }

        public Producto Producto { get; set; }

        public async Task OnGetAsync()
        {
            var _empresaId = Convert.ToInt32(User.FindFirst("EmpresaId").Value);

            Productos = await _context.Producto
                .Include(i => i.Empresa)
                .Where(x => x.EmpresaId == _empresaId)
                .ToListAsync();
        }
    }
}
