using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LIEA_System.Models
{
    public class DetalleVenta
    {
        [Key]
        public int Id_DetalleVenta { get; set; }

        [ForeignKey("Producto")]
        public int Id_Producto { get; set; }
        public Producto Producto { get; set; }

        [ForeignKey("Venta")]
        public int Id_Venta { get; set; }
        public Venta Venta { get; set; }

        public int Cantidad { get; set; }
        public decimal P_Unitario { get; set; }
        public decimal Total { get; set; }
    }
}
