using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
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

                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
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
                            "Hobbies = @Hobbies, Gender = @Gender, Country = @Country, IsDeleted=@IsDeleted WHERE Id = @Id", connection))
                        {
                            // Set parameters
                            updateCommand.Parameters.AddWithValue("@Id", employee.Id);
                            updateCommand.Parameters.AddWithValue("@Name", employee.Name);
                            updateCommand.Parameters.AddWithValue("@City", employee.City);
                            updateCommand.Parameters.AddWithValue("@Address", employee.Address);
                            updateCommand.Parameters.AddWithValue("@Hobbies", employee.Hobbies);
                            updateCommand.Parameters.AddWithValue("@Gender", employee.Gender);
                            updateCommand.Parameters.AddWithValue("@Country", employee.Country);
                            updateCommand.Parameters.AddWithValue("@IsDeleted", false);
                        // Execute the command
                        updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insert new employee

                        using (SqlCommand insertCommand = new SqlCommand(
                            "INSERT INTO Employee12 (Name, City, Address, Hobbies, Gender, Country,IsDeleted) " +
                            "VALUES (@Name, @City, @Address, @Hobbies, @Gender, @Country,@IsDeleted); SELECT SCOPE_IDENTITY();", connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Name", employee.Name);
                            insertCommand.Parameters.AddWithValue("@City", employee.City);
                            insertCommand.Parameters.AddWithValue("@Address", employee.Address);
                            insertCommand.Parameters.AddWithValue("@Hobbies", employee.Hobbies);
                            insertCommand.Parameters.AddWithValue("@Gender", employee.Gender);
                            insertCommand.Parameters.AddWithValue("@Country", employee.Country);
                            insertCommand.Parameters.AddWithValue("@IsDeleted", false);
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
        public int DeleteEmployee(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    //using (SqlCommand deleteCommand = new SqlCommand("Delete_Employee12", connection))
                    //{

                    //        deleteCommand.CommandType = CommandType.StoredProcedure;
                    //        deleteCommand.Parameters.AddWithValue("@Id", id);
                    //        deleteCommand.ExecuteNonQuery(); 

                    //}
                    using (SqlCommand deleteCommand = new SqlCommand("UPDATE Employee12 SET IsDeleted = @isDeleted where Id=@Id", connection))
                    {

                        //deleteCommand.CommandType = CommandType.StoredProcedure;
                        deleteCommand.Parameters.AddWithValue("@Id", id);
                        deleteCommand.Parameters.AddWithValue("@isDeleted", true);
                        deleteCommand.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions or log them as needed
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return id;
        }

        public void UpdateEmployee(EmployeeModelCrud employee)
        {
            throw new NotImplementedException();
        }

        public EmployeeModelCrud GetEmployeeById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Fetch the employee by ID
                using (SqlCommand selectCommand = new SqlCommand(
                    "SELECT * FROM Employee12 WHERE Id = @Id", connection))
                {
                    selectCommand.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Map data from the database to the EmployeeModelCrud object
                            return new EmployeeModelCrud
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

                // Return null if no employee is found with the given ID
                return null;
            }
        }
        
    }
}
