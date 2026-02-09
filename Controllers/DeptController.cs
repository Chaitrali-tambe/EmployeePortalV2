using EmployeePortal.Data;
using EmployeePortal.Models;
using EmployeePortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Controllers
{
    public class DeptController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public DeptController(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public IActionResult ViewDept()
        {
            var employees = _dbContext.EmployeesDB.ToList();
            ViewBag.EmployeeList = new SelectList(employees, "EmployeeID", "EmployeeName");
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> getDeptList()
        {
            var dept = await _dbContext.DepartmentTest.ToListAsync();
            return View(dept);
        }


        public IActionResult AddDept()
        {
            var model = new AddDeptViewModel();
            ViewBag.Employees = _dbContext.EmployeesDB
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDept(AddDeptViewModel model)
        {
            //var dept = new Department
            //{
            //    DeptName = model.DeptName,
            //    DeptHead = model.DeptHead

            //};
            //if (ModelState.IsValid) {
                

            //    _dbContext.DepartmentTest.Add(model);
            //}
            ViewBag.EmployeeList = new SelectList(_dbContext.EmployeesDB, "Id", "Name", model.DeptHead);
            return View(model);
            //return View();
        }
    }
}
