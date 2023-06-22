using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace surveyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionIdController : Controller
    {
        private readonly IConfiguration Configuration;
        public QuestionIdController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        

        [HttpGet]
        public string GetQuestionById(int id)
        {
            SqlConnection connection = new SqlConnection(Configuration["ConnectionStrings:DevConnection"]);
            SqlCommand command = new SqlCommand("SELECT qLabel, id FROM questions WHERE id=@id", connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();
            return JsonConvert.SerializeObject(table);
        }
    }
}
