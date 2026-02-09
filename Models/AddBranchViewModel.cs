using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortal.Models
{
    [Table("BranchTest")]
    public class AddBranchViewModel
    {
        public string? BrName { get; set; }
        public string? CityCode { get; set; }
        public string? City { get; set; }

    }
}
