using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tienda.Models
{
    public partial class Clientes
    {
        public Clientes()
        {
            Ventas = new HashSet<Ventas>();
        }
        [Key]
        public int IdCliente { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [Column("DUI")]
        [StringLength(10)]
        public string Dui { get; set; }
        [Required]
        [Column("NIT")]
        [StringLength(17)]
        public string Nit { get; set; }
        [Required]
        [StringLength(250)]
        public string Correo { get; set; }

        [InverseProperty("Cliente")]
        public virtual ICollection<Ventas> Ventas { get; set; }
    }
}
