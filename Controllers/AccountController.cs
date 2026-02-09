using EmployeePortal.Data;
using EmployeePortal.Models;
using EmployeePortal.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;

namespace EmployeePortal.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly AuthDbContext _authDbContext;
        public AccountController(AuthDbContext auth)
        {
            _authDbContext = auth;
        }

        //For Registration 
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model) 
        {
            if (!ModelState.IsValid)
                return View(model);


            if(_authDbContext.User1233.Any(u => u.UserName == model.UserName))
            {
                ModelState.AddModelError("","This username already exist");
                return View(model);
            }

            if(model.Password != null && model.ConfirmPassword != null)
            {
                if (model.Password == model.ConfirmPassword) {
                    var user = new User
                    {
                        Name = model.Name,
                        Email = model.Email,
                        UserName = model.UserName,
                        Password = model.Password

                    };

                    _authDbContext.Add(user);
                    _authDbContext.SaveChanges();

                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Password and Confirm Password Does not match");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Password or Confirm Password is not entered");
                return View(model);
            }

        }

        //For Login 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid) 
                return View(model);
        
            var user = _authDbContext.User1233.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);

            if (user == null) {
                ModelState.AddModelError("","Username or Password is incorrect");
                return View(model);
            }


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("FullName", user.Name),
                new Claim(ClaimTypes.Email, user.Email)

            };

            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}
