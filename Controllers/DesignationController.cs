using EmployeePortal.Data;
using EmployeePortal.Models;
using EmployeePortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeePortal.Controllers
{
    public class DesignationController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public DesignationController(ApplicationDbContext context) { 
            _dbContext = context;
        }
        public IActionResult ViewDesignations()
        {
            //var groupedData = _dbContext.DesignationTest.Include(d => d.department).AsEnumerable().GroupBy(d => d.department?.deparmentName ?? "Not Assigned").Select(g => new DesignationViewModel
            //{
            //    deparmentName = g.Key,
            //    designation = g.Select(d => d.DesignationName).ToList()
            //}).ToList();
            var groupedData = (from d in _dbContext.DesignationTest
                               join dpt in _dbContext.DepartmentTest on d.department equals dpt.DeptId.ToString()
                               group d by dpt.DeptName into g
                               select new
                               {
                                   deparmentName = g.Key,
                                   designation = g.ToList()
                               }).ToList();
            ViewBag.GroupedData = groupedData;
            return View();
        }

        [HttpGet]
        public IActionResult ViewDesignationlist()
        {
            var groupedData = _dbContext.DesignationTest.GroupBy(d => d.department).ToList();
            return View(groupedData);
        }


        public IActionResult AddDesignation()
        {
            var model = new AddDesignationViewModel();
            ViewBag.DepartmentList = _dbContext.DepartmentTest.Select(d => new SelectListItem
            {
                Value = d.DeptId.ToString(),
                Text = d.DeptName
            }).ToList(); 
                
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult AddDesignation(AddDesignationViewModel model)
        {
            if(_dbContext.DesignationTest.Any(dg => dg.desnName == model.desnName))
            {
                ModelState.AddModelError("", "Designation Already exist");
                ViewBag.DepartmentList = _dbContext.DepartmentTest.Select(d => new SelectListItem
                {
                    Value = d.DeptId.ToString(),
                    Text = d.DeptName
                }).ToList();
                return View(model);
            }

            if (ModelState.IsValid) {
                var designation = new Designation
                {
                    desnName = model.desnName,
                    department = model.department
                };

                _dbContext.DesignationTest.Add(designation);
                _dbContext.SaveChanges();
                return RedirectToAction("ViewDesignations");
            }

            ViewBag.DepartmentList = _dbContext.DepartmentTest.Select(d => new SelectListItem
            {
                Value = d.DeptId.ToString(),
                Text = d.DeptName
            }).ToList(); 
            return View(model);
        }
    }
}
