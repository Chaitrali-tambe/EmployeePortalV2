using EmployeePortal.Data;
using EmployeePortal.Migrations;
using EmployeePortal.Models;
using EmployeePortal.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.Threading.Tasks;

namespace EmployeePortal.Controllers
{
    [Authorize]
    public class BranchesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MastDbContext _mastdbContext;
        
        public BranchesController(ApplicationDbContext context, MastDbContext mastdbContext)
        {
            _dbContext = context;
            _mastdbContext = mastdbContext;
            //var dbName = _dbContext.Database.GetDbConnection().Database;
            //var dataSource = _dbContext.Database.GetDbConnection().DataSource;
        }

        [HttpGet]
        public ActionResult AddBranches()
        {
            ViewBag.Cities = GetCities();
            return View();
        }

        [HttpPost]
        public ActionResult AddBranches(AddBranchViewModel model) 
        {
            if(_dbContext.BranchTest.Any(b => b.BrName == model.BrName))
            {
                ModelState.AddModelError("", "This branch already exists");
                ViewBag.Cities = GetCities();
                return View(model);
            }

            //how to get dropdown value in one entity attribute and dropdown item text in otherentity attribute in mvc core while adding item

            var city = _mastdbContext.divbr.Where(c => c.citycode == model.CityCode)
                .Select(c => c.cityname).FirstOrDefault();

            var branch = new Branches
            {
                BrName = model.BrName,
                CityCode = model.CityCode,
                City = city
            };


            if (ModelState.IsValid) {

                _dbContext.Add(branch);
                _dbContext.SaveChanges();
                ModelState.Clear();
                ViewBag.Cities = GetCities();
                return View();
            }
            else
            {
                ViewBag.Cities = GetCities();
                return View(branch);
            }
            //return View(branch);



        }

        private List<SelectListItem> GetCities() { 
            return _mastdbContext.divbr
                .Select(c=> new { c.citycode, c.cityname })
                .GroupBy(c => c.citycode)
                .Select(g => new SelectListItem
                {
                    Value = g.FirstOrDefault().citycode,
                    Text = g.FirstOrDefault().cityname
                })
                .OrderBy(x => x.Text)
                .ToList();
        }



        [HttpGet]
        public async Task<IActionResult> ShowBranches()
        {
            var branches = await _dbContext.BranchTest.ToListAsync();
            return View(branches);
        }

    }
}
