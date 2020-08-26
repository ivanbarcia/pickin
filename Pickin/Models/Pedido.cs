//using Pickin.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pickin.Models
{
    [Table("Pedidos")]
    public class Pedido : AuditableEntity
    {
        [Required(ErrorMessage = "Debe ingresar un valor")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar un valor")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debe ingresar un valor")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe ingresar un valor")]
        public string DireccionNro { get; set; }

        public string Piso { get; set; }

        public string Depto { get; set; }

        public string Entrecalles { get; set; }

        [NotMapped]
        public string[] ProductosId { get; set; }

        [NotMapped]
        public string[] ProductosCantidad { get; set; }
        
        [Display(Name = "Productos")]
        public List<Pedidos_Productos> Productos { get; set; }

        public int CantidadTotal { get; set; }

        public decimal MontoTotal { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
    }

    public class Pedidos_Productos
    {
        [Key]
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Debe ingresar un valor")]
        public int Cantidad { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Debe ingresar un valor")]
        public decimal Precio { get; set; }

        public decimal Total { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Producto Producto { get; set; }
    }
}


