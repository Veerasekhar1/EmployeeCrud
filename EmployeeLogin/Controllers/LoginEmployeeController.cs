using EmployeeLogin.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLogin.Controllers
{
    public class LoginEmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CheckLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckLogin(EmployeeModel obj)
        {
            if(ModelState.IsValid)
            {
                EmployeeRepository emprepository = new EmployeeRepository();
                if (emprepository.CheckEmployee(obj.Name, obj.Password))
                {
                    return View(emprepository);
                }
                else
                {
                    ViewBag.message = "Invalid username Or password";
                    return View();
                }
            }
            return View();
        }
    }
}
