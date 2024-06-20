using System.ComponentModel.DataAnnotations;

namespace LIEA_System.Models
{
    public class Cliente
    {
        [Key]
        public int Id_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Correo_Electronico { get; set; }
        public string Telefono { get; set; }

        public ICollection<Venta> Ventas { get; set; }
    }
}
