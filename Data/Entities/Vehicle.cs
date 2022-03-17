using BlazorControlCefa.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorControlCefa.Data.Entities
{
    [Table("Vehiculo", Schema = "public")]
    public class Vehicle: AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Placa")]
        public string Plate { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Tipo Vehículo")]
        public int? VehicleTypeId { get; set; }

        [Display(Name = "Tipo Vehículo")]
        [ForeignKey("VehicleTypeId")]
        public VehicleType VehicleTypes { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Marca del Vehículo")]
        public int? VehicleBrandId { get; set; }

        [Display(Name = "Marca del Vehículo")]
        [ForeignKey("VehicleBrandId ")]
        public VehicleBrand VehicleBrands { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Color")]
        public string Color { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Tipo Dueño")]
        public string OwnerType { get; set; }
        
        [Display(Name = "Código del Vehículo")]
        public string VehicleCode { get; set; }
        
        [Display(Name = "Nombre de la Compañía")]
        public string CompanyName { get; set; }
    }
}
