using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TestStore.Models;

namespace TestStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class getnameController : ControllerBase
    {
        [HttpGet("{cat}")]
        public IEnumerable<usersaccounts> Get(string cat)
        {
            List<usersaccounts> li = new List<usersaccounts>();
            var builder = WebApplication.CreateBuilder();

            string conStr = builder.Configuration.GetConnectionString("TestStoreContext");
            SqlConnection conn1 = new SqlConnection(conStr);
            string sql;
            sql = "SELECT * FROM usersaccounts where role ='" + cat + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                li.Add(new usersaccounts
                {
                    name = (string)reader["name"],
                });

            }

            reader.Close();
            conn1.Close();
            return li;
        }
    }

}

