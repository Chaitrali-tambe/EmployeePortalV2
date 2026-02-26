using EmployeePortal.Models.Entities;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EmployeePortal.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> EmployeesDB { get; set; }
        public DbSet<Branches> BranchTest { get; set; }
        public DbSet<Department> DepartmentTest { get; set; }
        public DbSet<Designation> DesignationTest { get; set; }
    }

}
