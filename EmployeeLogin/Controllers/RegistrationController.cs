using EmployeeLogin.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLogin.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(RegisterModel obj)
        {
            RegisterRepository r = new RegisterRepository();
            r.AddUser(obj);
            //return View("AddUser");
            return RedirectToAction("CheckLogin", "LoginEmployee");

        }
    }
}
