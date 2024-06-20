using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LIEA_System.Models
{
    public class Compra
    {
        [Key]
        public int Id_Compra { get; set; }
        public DateTime Fecha_Compra { get; set; }
        public TimeSpan Hora_Compra { get; set; }

        [ForeignKey("Proveedor")]
        public int Id_Proveedor { get; set; }
        public Proveedor Proveedor { get; set; }

        public ICollection<DetalleCompra> DetallesCompra { get; set; }
        public decimal Total { get; set; }

    }
}
