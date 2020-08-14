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
        [Display(Name = "Productos")]
        [Required(ErrorMessage = "Debe ingresar un valor")]
        public List<Pedidos_Productos> Productos { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Debe ingresar un valor")]
        public int Cantidad { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessage = "Debe ingresar un valor")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Debe ingresar un valor")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
    }

    public class Pedidos_Productos
    {
        [Key]
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Producto Producto { get; set; }
    }
}


