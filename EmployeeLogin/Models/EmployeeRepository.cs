using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace EmployeeLogin.Models
{
    public class EmployeeRepository
    {
        public bool CheckEmployee(string name,string password)
        {
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string s = dbconfig["getconn:DefaultConnection"];
            bool flag = false;
            SqlConnection con = new SqlConnection(s);
            con.Open();
            SqlCommand cmd = new SqlCommand("Sp_User", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", name);
            cmd.Parameters.AddWithValue("@Password", password);
            flag = Convert.ToBoolean(cmd.ExecuteScalar());
            con.Close();
            return flag;
        }
    }
}
