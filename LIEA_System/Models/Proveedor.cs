using System.ComponentModel.DataAnnotations;

namespace LIEA_System.Models
{
    public class Proveedor
    {
        [Key]
        public int Id_Proveedor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }

        public ICollection<Producto> Productos { get; set; }
        public ICollection<Compra> Compras { get; set; }
    }
}
