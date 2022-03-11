using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tienda.Models
{
    public partial class Categorias
    {
        public Categorias()
        {
            Productos = new HashSet<Productos>();
        }
        
        [Key]
        public int IdCategoria { get; set; }

        [Required]
        [StringLength(50)]
        public string Categoria { get; set; }

        [InverseProperty("Categoria")]
        public virtual ICollection<Productos> Productos { get; set; }
    }
}
