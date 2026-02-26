using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortal.Models.Entities
{
    [Table("DIVBRNAME")]
    public class Cities
    {
        [Key]
        [Column("citycode")]
        public string? citycode {  get; set; }

        [Column("city")]
        public string? cityname {  get; set; }
    }
}
