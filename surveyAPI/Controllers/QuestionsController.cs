using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace surveyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=survey;Integrated Security=True");

        [HttpGet]
        public string GetAllQuestions()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM questions", connection);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();
            return JsonConvert.SerializeObject(table);
        }
    }
}
