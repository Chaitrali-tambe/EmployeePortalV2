using EmployeePortal.Data;
using EmployeePortal.Models;
using EmployeePortal.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.Collections.Generic;
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
            ViewBag.Designation = _dbContext.DesignationTest.Select(d => new SelectListItem
            {
                Value = d.desnName,
                Text = d.desnName
            });

            ViewBag.Branches = _dbContext.BranchTest.Select(b => new SelectListItem
            {
                Value = b.BrCode.ToString(),
                Text = b.BrName
            });

            ViewBag.Department = _dbContext.DepartmentTest.Select(dept => new SelectListItem
            {
                Value = dept.DeptId.ToString(),
                Text = dept.DeptName
            });

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
                DOB = viewModel.DOB,
                desnName = viewModel.desnName,
                Branch = viewModel.Branch,
                DeptId = viewModel.DeptId
            };
            if (ModelState.IsValid)
            {
                await _dbContext.EmployeesDB.AddAsync(emp);
                await _dbContext.SaveChangesAsync();
                ModelState.Clear();
                ViewBag.Designation = _dbContext.DesignationTest.Select(d => new SelectListItem
                {
                    Value = d.desnName,
                    Text = d.desnName
                });

                ViewBag.Branches = _dbContext.BranchTest.Select(b => new SelectListItem
                {
                    Value = b.BrCode.ToString(),
                    Text = b.BrName
                });
                return View();
            }
            return View(emp);
        }

        

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //var emps = await _dbContext.EmployeesDB.ToListAsync();
            var emps = await (from e in _dbContext.EmployeesDB
                              join dpt in _dbContext.DepartmentTest on e.DeptId equals dpt.DeptId into deptGroup
                              from dpt in deptGroup.DefaultIfEmpty()
                              join b in _dbContext.BranchTest on e.Branch equals b.BrCode into branchGroup
                              from b in branchGroup.DefaultIfEmpty()
                              select new EmployeeViewModel
                              {
                                  Id = e.Id,
                                  Name = e.Name,
                                  Age = e.Age,
                                  City = e.City,
                                  salary = e.salary,
                                  DOB = e.DOB,
                                  desnName = e.desnName,
                                  Branch = b.BrName,
                                  Dept = dpt.DeptName
                              }).ToListAsync();
            return View(emps);
        }

        [HttpGet]
        public IActionResult GetDeptByDesgn(string desgnName)
        {
            // Replace with your actual database logic
            var desn = _dbContext.DesignationTest.FirstOrDefault(d => d.desnName == desgnName);

            if (desn != null)
            {
                return Json(new { deptId = desn.department });
            }

            return Json(new { deptId = 0 });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var emp = await _dbContext.EmployeesDB.FindAsync(id);
            ViewBag.Designation = _dbContext.DesignationTest.Select(d => new SelectListItem
            {
                Value = d.desnName,
                Text = d.desnName
            });

            ViewBag.Branches = _dbContext.BranchTest.Select(b => new SelectListItem
            {
                Value = b.BrCode.ToString(),
                Text = b.BrName
            });

            ViewBag.Department = _dbContext.DepartmentTest.Select(dept => new SelectListItem
            {
                Value = dept.DeptId.ToString(),
                Text = dept.DeptName
            });
            return View(emp);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Employee viewModel)
        {
            var emp = await _dbContext.EmployeesDB.FindAsync(viewModel.Id);
            if (emp != null)
            {
                emp.Name = viewModel.Name;
                emp.Age = viewModel.Age;
                emp.City = viewModel.City;
                emp.salary = viewModel.salary;
                emp.DOB = viewModel.DOB;
                emp.desnName = viewModel.desnName;
                emp.Branch = viewModel.Branch;
                emp.DeptId = viewModel.DeptId;
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Employee");
        }


        [HttpPost]
        public async Task<ActionResult> Delete(Employee viewModel)
        {
            var emp = await _dbContext.EmployeesDB.FindAsync(viewModel.Id);
            if (emp != null)
            {
                _dbContext.EmployeesDB.Remove(emp);
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Employee");
        }
    }
}
