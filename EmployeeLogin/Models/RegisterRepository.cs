using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace EmployeeLogin.Models
{
    public class RegisterRepository
    {
        public void AddUser(RegisterModel obj)
        {
            var dbconfig = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string s = dbconfig["getconn:DefaultConnection"];
            SqlConnection con = new SqlConnection(s);
            SqlCommand cmd = new SqlCommand("Sp_UsersAdd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", obj.RuserName);
            cmd.Parameters.AddWithValue("@Password", obj.Rpassword);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
