using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeePortal.Models
{
    public class AddDesignationViewModel
    {
        public int desnId { get; set; }
        public string? desnName { get; set; }
        public string? department { get; set; }

        public int deptId { get; set; }

        public List<SelectListItem>? Department {  get; set; }
    }
}
