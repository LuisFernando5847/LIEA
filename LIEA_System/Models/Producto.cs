using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LIEA_System.Models
{
    public class Producto
    {
        [Key]
        public int Id_Producto { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(10, ErrorMessage = "El nombre no puede tener más de 10 caracteres")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Tipo es obligatorio")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "El Genero es obligatorio")]
        [RegularExpression(@"^[FMI]$", ErrorMessage = "El género solo puede ser 'M', 'F' o 'I'")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "La Talla es obligatoria")]
        public string Talla { get; set; }

        [Required(ErrorMessage = "El color es obligatorio")]
        public string Color { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Las exxistencias son obligatorias en caso de no ingresar existencias coloque 0")]
        public int Existencias { get; set; }

        [Required(ErrorMessage = "La imagen del producto es necesaria")]
        public byte[] Imagen { get; set; }

        [ForeignKey("Proveedor")]
        public int Id_Proveedor { get; set; }
        public Proveedor Proveedor { get; set; }        

        public ICollection<DetalleVenta> Ventas { get; set; }
        public ICollection<DetalleCompra> Compras { get; set; }
    }
}
