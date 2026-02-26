using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EmployeePortal.Models
{
    public class AddDeptViewModel
    {
        [Required]
        public string? DeptName { get; set; }

        [Required]
        public string? DeptHead { get; set; }

        public int SelectedEmployeeId { get; set; }

        public List<SelectListItem>? Employees { get; set; }
    }
}
