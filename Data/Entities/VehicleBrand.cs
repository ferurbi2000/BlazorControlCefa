using BlazorControlCefa.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorControlCefa.Data.Entities
{
    [Table("MarcaVehiculo", Schema = "public")]
    public class VehicleBrand : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Marca Vehiculo")]
        public string Name { get; set; }
    }
}
