
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortal.Models.Entities
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeptId { get; set; }

        [StringLength(50)]
        public string? DeptName { get; set; }

        [StringLength(200)]
        public int DeptHead { get; set; }

    }
}
