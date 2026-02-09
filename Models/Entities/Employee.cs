using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortal.Models.Entities
{
    public class Employee
    {
        [Key] // Marks this as the Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tells EF the DB handles the value
        public int Id { get; set; } 
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? City { get; set; }

        [Column(TypeName = "decimal(10,2)")] // Explicitly tells SQL the type
        public decimal salary { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

    }
}
