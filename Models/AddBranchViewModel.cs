using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortal.Models
{
    [Table("BranchTest")]
    public class AddBranchViewModel
    {
        public string? BrName { get; set; }
        public string? CityCode { get; set; }
        public string? City { get; set; }
        public IEnumerable<SelectListItem>? CityList { get; set; }

    }
}
