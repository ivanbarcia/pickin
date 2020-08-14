//using Pickin.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pickin.Models
{
    public class AuditableEntity : Entity
    {
        [ScaffoldColumn(false)]
        public int Estado { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha Alta")]
        [DisplayFormat(DataFormatString = "{0:MMMM yyyy}", ApplyFormatInEditMode = true)]
        [ScaffoldColumn(false)]
        public DateTime? FechaAlta { get; set; }

        [MaxLength(256)]
        [Display(Name = "Usuario Alta")]
        [ScaffoldColumn(false)]
        public string UsuarioAlta { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha Modificación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [ScaffoldColumn(false)]
        public DateTime? FechaModificacion { get; set; }

        [Display(Name = "Usuario Modificación")]
        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string UsuarioModificacion { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha Baja")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [ScaffoldColumn(false)]
        public DateTime? FechaBaja { get; set; }

        [Display(Name = "Usuario Baja")]
        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string UsuarioBaja { get; set; }
    }
}