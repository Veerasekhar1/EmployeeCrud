using Newtonsoft.Json;
using System.Reflection;

namespace EmployeeLogin.Models
{
    public class EmployeeModelCrud
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Hobbies { get; set; }
        //public  string? Gender { get; set; }
        public Gender Gender { get; set; }
        public string? Country { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public enum Gender
    {
        Male,
        Female,
        Unknown
    }
}
