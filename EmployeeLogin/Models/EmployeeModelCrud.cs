namespace EmployeeLogin.Models
{
    public class EmployeeModelCrud
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Hobbies { get; set; }
        public  string? Gender { get; set; }
        public string? Country { get; set; }

        public IEnumerable<CountryModel> CountriesList { get; set; }
        public string? SelectedCountryCode { get; set; }
    }
    public class CountryModel
    {
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
    }
}
