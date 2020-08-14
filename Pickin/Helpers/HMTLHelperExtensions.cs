using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Security.Claims;

namespace Pickin.Helpers
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this IHtmlHelper html, string page = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass)) 
                cssClass = "active";

            string currentPage = ((object[])html.ViewContext.RouteData.Values.Values)[0].ToString();

            if (String.IsNullOrEmpty(page))
                page = currentPage;

            return currentPage.Split("/")[1].Contains(page.Split("/")[1]) ? cssClass : String.Empty;
        }

        public static string PageClass(this IHtmlHelper html)
        {
            string currentPage = ((object[])html.ViewContext.RouteData.Values.Values)[0].ToString();
            return currentPage;
        }
    }
}
