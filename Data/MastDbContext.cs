using EmployeePortal.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortal.Data
{
    public class MastDbContext : DbContext
    {
        public MastDbContext(DbContextOptions<MastDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Cities> divbr {  get; set; }
    }
}
