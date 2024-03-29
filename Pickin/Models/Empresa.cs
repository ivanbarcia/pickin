﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Pickin.Resources;

namespace Pickin.Models
{
    [Table("Empresas")]
    public class Empresa : AuditableEntity
    {
        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string Celular { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        public string DescripcionCompleta
        {
            get { return string.Format("{0} - {1}", Codigo, Descripcion); }
            set { }
        }

        [Display(Name = "Imagen")]
        public byte[] Image { get; set; }
    }
}
