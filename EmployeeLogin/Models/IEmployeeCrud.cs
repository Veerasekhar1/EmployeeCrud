using System.Collections.Generic;
using System.Collections.Generic;

namespace EmployeeLogin.Models
{
    public interface IEmployeeCrud
    {
        EmployeeModelCrud GetEmployee(int id);
        IEnumerable<EmployeeModelCrud> GetEmployee();
        void SaveEmployee(EmployeeModelCrud employee);
        EmployeeModelCrud GetEmployeeById(int id);
       // void UpdateEmployee(EmployeeModelCrud employee);
        int DeleteEmployee(int id);

    }
}
