using BlazorControlCefa.Data.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorControlCefa.Data.Entities
{
    [Table("Visita", Schema = "public")]
    public class Visit : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Tipo Visita")]
        public int? VisitTypeId { get; set; }

        [Display(Name = "Tipo Visita")]
        [ForeignKey("VisitTypeId")]
        public VisitType VisitTypes { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Motivo Visita")]
        public int? ReasonId { get; set; }

        [Display(Name = "Motivo Visita")]
        [ForeignKey("ReasonId")]
        public Reason Reasons { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Persona a Visitar")]
        public int? PersonVisitToId { get; set; }

        // Revisar si funciona con PersonId
        [Display(Name = "Persona a Visitar")]
        [ForeignKey("PersonVisitToId")]
        public Person PersonVisitTos { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Persona que aprueba")]
        public int? PersonApproverId { get; set; }

        // Revisar si funciona con PersonId
        [Display(Name = "Persona que Aprueba")]
        [ForeignKey("PersonApproverId")]
        public Person PersonApprovers { get; set; }

        [Required]
        public int? DepartmentId { get; set; }

        [Display(Name = "Departamento")]
        [ForeignKey("DepartmentId")]
        public Department Departments { get; set; }

        [Display(Name = "Número de Pase")]
        public string PassNumber { get; set; }

        [Display(Name = "Doc Salida")]
        public string DocOut { get; set; }

        [Display(Name = "Comentario")]
        public string Note { get; set; }

        [Display(Name = "Fecha Ingreso")]
        public DateTime? CheckIn { get; set; }
        
        [Display(Name = "Fecha Salida")]
        public DateTime? CheckOut { get; set; }

        [DefaultValue(true)]
        [Display(Name = "Is CheckIn")]
        public bool IsCheckIn { get; set; }

        public ICollection<VisitPersonDetail> VisitDetails { get; set; }        

    }
}
