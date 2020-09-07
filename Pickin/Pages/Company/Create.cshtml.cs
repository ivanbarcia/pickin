using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pickin.Models;

namespace Pickin.Pages.Company
{
    public class CreateModel : PageModel
    {
        private readonly Pickin.Data.ApplicationDbContext _context;

        public CreateModel(Pickin.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pedido Input { get; set; }

        [BindProperty]
        public IList<Producto> Productos { get; set; }

        public IActionResult OnGet(string name)
        {
            if (name == string.Empty)
            {
                return BadRequest();
            }

            var empresa = _context.Empresa.Where(x => x.Codigo == name);

            if (!empresa.Any())
            {
                return BadRequest();
            }

            var empresaId = empresa.FirstOrDefault().Id;

            Input = new Pedido { EmpresaId = empresaId };

            Productos = _context.Producto.Where(x => x.EmpresaId == empresaId).ToList();

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

                var detalle_productos = string.Empty;

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

                    detalle_productos = detalle_productos + string.Format("(${0}) {1} x {2}", producto.Precio, producto.Cantidad, _context.Producto.Find(producto.ProductoId).Descripcion) + "\n";

                    Input.Productos.Add(producto);
                }

                Input.CantidadTotal = Input.Productos.Sum(x => x.Cantidad);
                Input.MontoTotal = Input.Productos.Sum(x => x.Total);
                Input.Fecha = DateTime.Now;

                _context.Pedido.Add(Input);
                await _context.SaveChangesAsync();

                var empresa = _context.Empresa.Find(Input.EmpresaId);

                var textMessage = string.Format("Hola!\n Este es el pedido *{9}{10}*.\n\n A nombre de: *{1}, {0}*.\n\n Dirección de envío: *{2} {3}*\n Aclaracion: *{4} / {5}*.\n Entre calles: *{6}*.\n\n Detalle del pedido: {7}\n *Total Pedido: ${8}*", 
                    Input.Nombre, Input.Apellido, Input.Direccion, Input.DireccionNro, Input.Piso, Input.Depto, Input.Entrecalles, detalle_productos, Input.MontoTotal, empresa.Id, Input.Id.ToString("D4"));

                var textEncoded = HttpUtility.UrlEncode(textMessage);

                var external_message = string.Format("https://wa.me/{0}?text={1}", empresa.Celular, textEncoded);
                
                return Redirect(external_message);
            }

            Productos = _context.Producto.ToList();

            return Page();
        }
    }
}
