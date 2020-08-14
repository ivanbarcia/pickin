using Pickin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Pickin.Data.ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, Pickin.Data.ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGet()
        {
        
        }
    }
}
