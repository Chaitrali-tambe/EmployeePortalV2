using EmployeePortal.Data;
using EmployeePortal.Models;
using EmployeePortal.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeePortal.Controllers
{
 [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel viewModel)
        {
            // Calculate age based on DOB
            var today = DateTime.Now;
            var age = today.Year - viewModel.DOB.Year;

            if (viewModel.DOB.Date > today.AddYears(-age))
                age--;

            var emp = new Employee
            {
                Name = viewModel.Name,
                Age = age,
                City = viewModel.City,
                salary = viewModel.salary,
                DOB = viewModel.DOB

            };
            if (ModelState.IsValid)
            {
                await _dbContext.EmployeesDB.AddAsync(emp);
                await _dbContext.SaveChangesAsync();
                ModelState.Clear();
                return View();
            }
            return View(emp);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var emps = await _dbContext.EmployeesDB.ToListAsync();
            return View(emps);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var emp = await _dbContext.EmployeesDB.FindAsync(id);
            return View(emp);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Employee viewModel)
        {
            var emp = await _dbContext.EmployeesDB.FindAsync(viewModel.Id);
            if(emp != null)
            {
                emp.Name = viewModel.Name;
                emp.Age = viewModel.Age;
                emp.City = viewModel.City;
                emp.salary = viewModel.salary;
                emp.DOB = viewModel.DOB;
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Employee");
        }


        [HttpPost]
        public async Task<ActionResult> Delete(Employee viewModel) {
            var emp = await _dbContext.EmployeesDB.FindAsync(viewModel.Id);
            if( emp != null)
            {
                _dbContext.EmployeesDB.Remove(emp);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Employee");
        }
    }
}
