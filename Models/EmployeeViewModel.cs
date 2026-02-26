using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortal.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? City { get; set; }
        public decimal salary { get; set; }
        public DateTime DOB { get; set; }
        public string? desnName { get; set; }
        public string? Branch { get; set; }
        public string? Dept { get; set; }
    }
}
