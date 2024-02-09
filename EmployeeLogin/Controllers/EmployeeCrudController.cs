using EmployeeLogin.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLogin.Controllers
{
    public class EmployeeCrudController : Controller
    {
        private readonly IEmployeeCrud _employeeRepository;

        public EmployeeCrudController(IEmployeeCrud employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        
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
            if (ModelState.IsValid)
            {
                _employeeRepository.SaveEmployee(employee);
                 return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }
        public IActionResult Edit(EmployeeModelCrud id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeRepository.GetEmployee();

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeModelCrud employee)
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
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetEmployee();
            return View("Index");

        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeeRepository.GetEmployee(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeeRepository.DeleteEmployee(id); 
            return RedirectToAction(nameof(Index));
        }
    }
}
