using EmployeeLogin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EmployeeLogin.Controllers
{
    public class LoginEmployeeController : Controller
    {
        private readonly IEmployeeCrud _employeeRepository;

        public LoginEmployeeController(IEmployeeCrud employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

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
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeRepository emprepository = new EmployeeRepository();
                    if (emprepository.CheckEmployee(obj.Name, obj.Password))
                    {
                        var employees = _employeeRepository.GetEmployee();

                        return View("/Views/EmployeeCrud/Index.cshtml",employees);
                    }
                    else
                    {
                        ViewBag.message = "Invalid username Or password";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here, you might want to log it or take appropriate action
                ViewBag.message = "An error occurred: " + ex.Message;
                return View();
            }
            return View();
        }
    }
}
