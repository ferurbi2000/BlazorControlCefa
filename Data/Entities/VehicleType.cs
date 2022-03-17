using BlazorControlCefa.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorControlCefa.Data.Entities
{
    [Table("TipoVehiculo", Schema = "public")]
    public class VehicleType : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Tipó Vehiculo")]
        public string Name { get; set; }
    }
}
