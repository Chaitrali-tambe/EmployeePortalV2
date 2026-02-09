using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortal.Models.Entities
{
    [Table("BranchTest")]
    public class Branches
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrCode { get; set; }

        [Required]
        [StringLength(100)]
        public string? BrName { get; set; }

        [Required,StringLength(10)]
        public string? CityCode { get; set; }

        [Required,StringLength(100)]
        public string? City { get; set; }


    }
}
