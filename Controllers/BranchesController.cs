using EmployeePortal.Data;
using EmployeePortal.Migrations;
using EmployeePortal.Models;
using EmployeePortal.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeePortal.Controllers
{
    [Authorize]
    public class BranchesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        
        public BranchesController(ApplicationDbContext context)
        {
            _dbContext = context;
            //var dbName = _dbContext.Database.GetDbConnection().Database;
            //var dataSource = _dbContext.Database.GetDbConnection().DataSource;
        }

        [HttpGet]
        public ActionResult AddBranches()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBranches(AddBranchViewModel model) 
        {
            if(_dbContext.BranchTest.Any(b => b.BrName == model.BrName))
            {
                ModelState.AddModelError("", "This branch already exists");
                return View(model);
            }

            var branch = new Branches
            {
                BrName = model.BrName,
                City = model.City,
                CityCode = model.CityCode
            };


            if (ModelState.IsValid) {

                _dbContext.Add(branch);
                _dbContext.SaveChanges();
                ModelState.Clear();
                return View();
            }
            else
            {
                return View(branch);
            }
            //return View(branch);



        }


        [HttpGet]
        public async Task<IActionResult> ShowBranches()
        {
            var branches = await _dbContext.BranchTest.ToListAsync();
            return View(branches);
        }

    }
}
