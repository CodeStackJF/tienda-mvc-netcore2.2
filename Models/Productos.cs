using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tienda.Models
{
    public partial class Productos
    {
        public Productos()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
        }
        [Key]
        public int IdProducto { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(150)]
        public string Descripcion { get; set; }
        [Required]
        [StringLength(50)]
        public string Marca { get; set; }
        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }
        
        [Required]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        [InverseProperty("Productos")]
        public virtual Categorias Categoria { get; set; }

        [InverseProperty("Producto")]
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
