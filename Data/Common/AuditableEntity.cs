using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorControlCefa.Data.Common
{
    public class AuditableEntity: IAuditableEntity, IActivableEntity, ISoftDeletable
    {        
        public string CreatedBy { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public string LastModifiedBy { get; set; }
        
        public DateTime? LastModified { get; set; }
        
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        
        [DefaultValue(false)]
        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedDate { get; set; }
    }
}
