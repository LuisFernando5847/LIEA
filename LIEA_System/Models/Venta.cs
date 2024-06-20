using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LIEA_System.Models
{
    public class Venta
    {
        [Key]
        public int Id_Venta { get; set; }
        public DateTime Fecha_Venta { get; set; }
        public TimeSpan Hora_Venta { get; set; }

        [ForeignKey("Cliente")]
        public int Id_Cliente { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<DetalleVenta> DetallesVenta { get; set; } 
        public decimal Total { get; set; }
    }
}
