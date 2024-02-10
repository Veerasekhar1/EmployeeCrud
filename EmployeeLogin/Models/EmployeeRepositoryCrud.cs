using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeeLogin.Models
{
    public class EmployeeRepositoryCrud : IEmployeeCrud
    {
        private readonly string _connectionString;

        public EmployeeRepositoryCrud(string connectionString)
        {
            Console.WriteLine($"Connection String: {connectionString}");
            _connectionString = connectionString;
        }
        

        public EmployeeModelCrud GetEmployee(int id)
        {
            EmployeeModelCrud employee = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Employee12 WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                employee = new EmployeeModelCrud
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    City = reader["City"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    Hobbies = reader["Hobbies"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    Country = reader["Country"].ToString()
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions or log them as needed
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return employee;
        }

        public IEnumerable<EmployeeModelCrud> GetEmployee()
        {
            //EmployeeModelCrud employee = null;
            List<EmployeeModelCrud> employees = new List<EmployeeModelCrud>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Display_Employee12", connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                EmployeeModelCrud employee = new EmployeeModelCrud
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    City = reader["City"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    Hobbies = reader["Hobbies"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    Country = reader["Country"].ToString()
                                };
                                employees.Add(employee);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions or log them as needed
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return employees;
        }

        public string? GetEmployee(object value)
        {
            throw new NotImplementedException();
        }

        public void SaveEmployee(EmployeeModelCrud employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                //try
                //{
                    connection.Open();

                    if (employee.Id != 0)
                    { 
                        // Update existing employee
                        using (SqlCommand updateCommand = new SqlCommand(
                            "UPDATE Employee12 SET Name = @Name, City = @City, Address = @Address, " +
                            "Hobbies = @Hobbies, Gender = @Gender, Country = @Country WHERE Id = @Id", connection))
                        {
                            // Set parameters
                            updateCommand.Parameters.AddWithValue("@Id", employee.Id);
                            updateCommand.Parameters.AddWithValue("@Name", employee.Name);
                            updateCommand.Parameters.AddWithValue("@City", employee.City);
                            updateCommand.Parameters.AddWithValue("@Address", employee.Address);
                            updateCommand.Parameters.AddWithValue("@Hobbies", employee.Hobbies);
                            updateCommand.Parameters.AddWithValue("@Gender", employee.Gender);
                            updateCommand.Parameters.AddWithValue("@Country", employee.Country);

                            // Execute the command
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insert new employee

                        using (SqlCommand insertCommand = new SqlCommand(
                            "INSERT INTO Employee12 (Name, City, Address, Hobbies, Gender, Country) " +
                            "VALUES (@Name, @City, @Address, @Hobbies, @Gender, @Country); SELECT SCOPE_IDENTITY();", connection))
                        {
                            //Guid newEmployeeId = Guid.NewGuid();
                            //employee.Id = Guid.NewGuid().ToString();
                            // Set parameters
                           // insertCommand.Parameters.AddWithValue("@Id", newEmployeeId);
                            insertCommand.Parameters.AddWithValue("@Name", employee.Name);
                            insertCommand.Parameters.AddWithValue("@City", employee.City);
                            insertCommand.Parameters.AddWithValue("@Address", employee.Address);
                            insertCommand.Parameters.AddWithValue("@Hobbies", employee.Hobbies);
                            insertCommand.Parameters.AddWithValue("@Gender", employee.Gender);
                            insertCommand.Parameters.AddWithValue("@Country", employee.Country);
                            //Guid insertedEmployeeId = (Guid)insertCommand.ExecuteScalar();
                            //employee.Id = insertedEmployeeId;
                            //employee.Id = convertedEmployeeId;
                            // Execute the command and get the new employee ID
                              int newEmployeeId = Convert.ToInt32(insertCommand.ExecuteScalar());
                        }
                    }
                //}
                //catch (Exception ex)
                //{
                //    // Handle exceptions or log them as needed
                //    Console.WriteLine($"Error: {ex.Message}");
                //}
            }
        }
        public void DeleteEmployee(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand deleteCommand = new SqlCommand("DELETE FROM Employee12 WHERE Id = @Id", connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@Id", id);
                        deleteCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions or log them as needed
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
