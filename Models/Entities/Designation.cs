using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortal.Models.Entities
{
    public class Designation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int desnId { get; set; }

        [Required]
        [StringLength(100)]
        public string? desnName { get; set; }

        [Required]
        [StringLength(50)]
        public string? department { get; set; }
    }
}
