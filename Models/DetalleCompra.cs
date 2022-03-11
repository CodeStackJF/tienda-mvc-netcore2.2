using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tienda.Models
{
    public partial class DetalleCompra
    {
        [Key]
        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }

        [Required(ErrorMessage="Seleccione un producto.")]
        public int? IdProducto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Column(TypeName = "money")]
        [Required]
        public decimal Precio { get; set; }
        [Column("IVA", TypeName = "money")]
        public decimal Iva { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        [ForeignKey("IdCompra")]
        [InverseProperty("DetalleCompra")]
        public virtual Compras Compra { get; set; }
        [ForeignKey("IdProducto")]

        public virtual Productos Producto { get; set; }
    }
}
