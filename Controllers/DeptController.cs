using EmployeePortal.Data;
using EmployeePortal.Models;
using EmployeePortal.Models.Entities;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeePortal.Controllers
{

    public class DeptController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public DeptController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> ViewDept()
        {
            // var employees = _dbContext.EmployeesDB.ToList();
            //ViewBag.EmployeeList = new SelectList(employees, "EmployeeID", "EmployeeName");


            var dept = await (from d in _dbContext.DepartmentTest
                              join e in _dbContext.EmployeesDB on d.DeptHead equals e.Id.ToString() into empgroup
                              from e in empgroup.DefaultIfEmpty()
                              select new Department
                              {
                                  DeptId = d.DeptId,
                                  DeptName = d.DeptName,
                                  DeptHead = e != null ? e.Name : "No Head Assigned"
                              }).ToListAsync();
            return View(dept);
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
        [IgnoreAntiforgeryToken]
        public IActionResult AddDept(AddDeptViewModel model)
        {

            if (_dbContext.DepartmentTest.Any(d => d.DeptName == model.DeptName))
            {
                ModelState.AddModelError("", "This Department already exists");
                ViewBag.Employees = _dbContext.EmployeesDB
               .Select(e => new SelectListItem
               {
                   Value = e.Id.ToString(),
                   Text = e.Name
               })
               .ToList();
                return View(model);
            }
            //var rawValue = form["DeptHeadId"];
            else if (ModelState.IsValid)
            {
                var dept = new Department
                {
                    DeptName = model.DeptName,
                    DeptHead = model.DeptHead

                };
                _dbContext.DepartmentTest.Add(dept);
                _dbContext.SaveChanges();
                return RedirectToAction("ViewDept");
                //return View(model);
            }
            ViewBag.Employees = _dbContext.EmployeesDB
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Name
                })
                .ToList();
            return View(model);
            //return View();
        }
    }
}
