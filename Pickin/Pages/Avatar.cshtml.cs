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
    public class AvatarModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public AvatarModel(Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            // to get the user details to load user Image    
            var user = await _context.ApplicationUser.FindAsync(id);

            return new FileContentResult(user.Image, "image/jpeg");
        }
    }
}
