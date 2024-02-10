using System.Collections.Generic;
using System.Collections.Generic;

namespace EmployeeLogin.Models
{
    public interface IEmployeeCrud
    {
        EmployeeModelCrud GetEmployee(int id);
        //object GetEmployee();
        IEnumerable<EmployeeModelCrud> GetEmployee();
        string? GetEmployee(object value);
        void SaveEmployee(EmployeeModelCrud employee);
        void DeleteEmployee(int id);

    }
}
