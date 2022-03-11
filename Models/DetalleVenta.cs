using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tienda.Models
{
    public partial class DetalleVenta
    {
        [Key]
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "money")]
        public decimal Precio { get; set; }
        [Column("IVA", TypeName = "money")]
        public decimal Iva { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        [ForeignKey("IdProducto")]
        [InverseProperty("DetalleVenta")]
        public virtual Productos Producto { get; set; }

        [ForeignKey("IdVenta")]
        [InverseProperty("DetalleVenta")]
        public virtual Ventas Venta { get; set; }
    }
}
