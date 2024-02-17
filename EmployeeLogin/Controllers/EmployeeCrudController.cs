using EmployeeLogin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeLogin.Controllers
{
    public class EmployeeCrudController : Controller
    {
        private readonly IEmployeeCrud _employeeRepository;

        public EmployeeCrudController(IEmployeeCrud employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.GetEmployee();
            return View("Index", employees);
        }

        public IActionResult Details(int id)
        {
            EmployeeModelCrud employee = _employeeRepository.GetEmployee(id);

            if (employee == null)
            {
                return NotFound(); // Return a 404 Not Found if the employee is not found
            }

            return View(employee);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeModelCrud employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeRepository.SaveEmployee(employee);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                ViewBag.message = "RECOR IS NOT INSERT: " + ex.Message;
                return View(nameof(Index));
            }

            return View(employee);
        }
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var existingEmployee = _employeeRepository.GetEmployeeById(id);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            return View(existingEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeModelCrud employee,int id1)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _employeeRepository.SaveEmployee(employee);
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
          
            int deletedEmployeeId = _employeeRepository.DeleteEmployee(id.Value);

            if (deletedEmployeeId == 0)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
