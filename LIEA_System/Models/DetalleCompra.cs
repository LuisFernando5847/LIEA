using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LIEA_System.Models
{
    public class DetalleCompra
    {
        [Key]
        public int Id_DetalleCompra { get; set; }

        [ForeignKey("Producto")]
        public int Id_Producto { get; set; }
        public Producto Producto { get; set; }

        [ForeignKey("Compra")]
        public int Id_Compra { get; set; }
        public Compra Compra { get; set; }

        public int Cantidad { get; set; }
        public decimal P_Unitario { get; set; }
        public decimal Total { get; set; }
    }
}
