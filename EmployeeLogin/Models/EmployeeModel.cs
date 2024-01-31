using System.ComponentModel.DataAnnotations;

namespace EmployeeLogin.Models
{
    public class EmployeeModel
    {
        [Required]
        public String? Name { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
