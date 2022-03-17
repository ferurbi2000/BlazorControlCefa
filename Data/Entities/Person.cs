using BlazorControlCefa.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorControlCefa.Data.Entities
{
    [Table("Persona", Schema = "public")]
    public class Person : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Tipo Persona")]
        public int? PersonTypeId { get; set; }

        [Display(Name = "Tipo Persona")]
        [ForeignKey("PersonTypeId")]
        public PersonType PersonTypes { get; set; }

        [Required(ErrorMessage = "El campo de {0} es requerido")]
        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [Required]
        public int? DepartmentId { get; set; }

        [Display(Name = "Departamento")]
        [ForeignKey("DepartmentId")]
        public Department Departments { get; set; }

        [Required]
        public int? PositionId { get; set; }

        [Display(Name = "Puesto")]
        [ForeignKey("PositionId")]
        public Position Positions { get; set; }

        [Display(Name = "Compañia")]
        public string CompannyName { get; set; }

    }
}
