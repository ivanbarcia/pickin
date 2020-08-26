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
    public class Create2Model : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public Create2Model(Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pedido Input { get; set; }

        [BindProperty]
        public IList<Producto> Productos { get; set; }

        public IActionResult OnGet()
        {
            Input = new Pedido();

            Productos = _context.Producto.ToList();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (Input.ProductosId.Length <= 0)
            {
                ModelState.AddModelError("", "Debe ingresar algun producto para continuar");
            }

            if (ModelState.IsValid)
            {
                Input.Productos = new List<Pedidos_Productos>();
                var productos = Input.ProductosId[0].Split(",");
                var cantidades = Input.ProductosCantidad[0].Split(",");

                for (var i = 0; i < productos.Length; i++)
                {
                    var precio_unidad = _context.Producto.Find(Convert.ToInt32(productos[i])).Precio;

                    var producto = new Pedidos_Productos
                    {
                        ProductoId = Convert.ToInt32(productos[i]),
                        Cantidad = Convert.ToInt32(cantidades[i]),
                        Precio = precio_unidad,
                        Total = Convert.ToInt32(cantidades[i]) * precio_unidad
                    };

                    Input.Productos.Add(producto);
                }

                Input.CantidadTotal = Input.Productos.Sum(x => x.Cantidad);
                Input.MontoTotal = Input.Productos.Sum(x => x.Total);
                Input.Fecha = DateTime.Now;

                _context.Pedido.Add(Input);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            Productos = _context.Producto.ToList();

            return Page();
        }
    }
}
