using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortal.Models.Entities
{
    public class User
    {
        [Key] // Marks this as the Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [StringLength(50)]
        public string? UserName { get; set; }
        
        [Required]
        [StringLength(30)]
        public string? Password { get; set; }

        [Required]
        [StringLength(50)]
        public string? Email { get; set; }

    }
}
