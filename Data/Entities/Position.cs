using BlazorControlCefa.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorControlCefa.Data.Entities
{
    [Table("Puesto", Schema = "public")]
    public class Position: AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Puesto")]
        public string Name { get; set; }
    }
}
