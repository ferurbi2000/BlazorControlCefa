using BlazorControlCefa.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorControlCefa.Data.Entities
{
    [Table("DetalleVehiculo", Schema = "public")]
    public class VehicleDetail : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Persona")]
        public int? PersonId { get; set; }

        [Display(Name = "Persona")]
        [ForeignKey("PersonId")]
        public Person Persons { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Vehículo")]
        public int? VehicleId { get; set; }

        [Display(Name = "Vehículo")]
        [ForeignKey("VehicleId")]
        public Vehicle Vehicles { get; set; }
    }
}
