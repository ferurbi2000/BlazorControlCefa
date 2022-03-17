using BlazorControlCefa.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorControlCefa.Data.Entities
{
    [Table("DetalleVisitaVehiculo", Schema = "public")]
    public class VisitVehicleDetail : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Visita")]
        public int? VisitId { get; set; }

        [Display(Name = "Visita")]
        [ForeignKey("VisitId")]
        public Visit Visits { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Vehiculo")]
        public int? VehicleId { get; set; }

        [Display(Name = "Vehiculos")]
        [ForeignKey("VehicleId")]
        public Vehicle Vehicles { get; set; }
    }
}
