using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Pickin.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }

        [PersonalData]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [PersonalData]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        public virtual Empresa Empresa { get; set; }

        [Display(Name = "Avatar")]
        public byte[] Image { get; set; }
    }

    public static class IdentityExtensions
    {
        public static string FirstName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.GivenName);
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string LastName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Surname);
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
