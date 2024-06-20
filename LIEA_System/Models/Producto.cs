using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LIEA_System.Models
{
    public class Producto
    {
        [Key]
        public int Id_Producto { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Genero { get; set; }
        public string Talla { get; set; }
        public string Color { get; set; }
        public string Modelo { get; set; }
        public decimal Precio { get; set; }
        public int Existencias { get; set; }
        public byte[] Imagen { get; set; }

        [ForeignKey("Proveedor")]
        public int Id_Proveedor { get; set; }
        public Proveedor Proveedor { get; set; }        

        public ICollection<DetalleVenta> Ventas { get; set; }
        public ICollection<DetalleCompra> Compras { get; set; }
    }
}
