//using Pickin.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pickin.Models
{
    [Table("Productos")]
    public class Producto : AuditableEntity
    {
        [Required(ErrorMessage = "Debe ingresar un valor")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar un valor")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe ingresar un valor")]
        //[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        //[Range(1, 100000000)]
        //[DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        [NotMapped]
        public int? Cantidad { get; set; }

        [Required(ErrorMessage = "Debe ingresar un valor")]
        public int Stock { get; set; }

        [Display(Name = "Imagen")]
        public byte[] Image { get; set; }

        public int EmpresaId { get; set; }

        public virtual Empresa Empresa { get; set; }
    }
}


