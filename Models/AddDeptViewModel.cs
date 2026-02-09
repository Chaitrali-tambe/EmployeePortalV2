using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeePortal.Models
{
    public class AddDeptViewModel
    {
        public string? DeptName { get; set; }
        public string? DeptHead { get; set; }
        public int SelectedEmployeeId { get; set; }
        public List<SelectListItem> Employees { get; set; }
    }
}
