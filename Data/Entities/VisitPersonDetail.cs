using BlazorControlCefa.Data.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorControlCefa.Data.Entities
{
    [Table("DetalleVisitaPersona", Schema = "public")]
    public class VisitPersonDetail : AuditableEntity
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
        [Display(Name = "Persona")]
        public int? PersonId { get; set; }

        [Display(Name = "Persona")]
        [ForeignKey("PersonId")]
        public Person Persons { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Is Principal")]
        public bool IsPrincipal { get; set; }
    }
}
