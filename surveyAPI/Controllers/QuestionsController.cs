using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

// create class questions - list of type options
// create class options

// jsonconvert.deserialize


namespace surveyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        public QuestionsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public string GetAllQuestions()
        {
            SqlConnection connection = new SqlConnection(Configuration["ConnectionStrings:DevConnection"]);
            SqlCommand command = new SqlCommand("SELECT * FROM questions", connection);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            foreach(DataRow dataRow in table.Rows)
            {
                foreach(var item in dataRow.ItemArray)
                {
                    Console.WriteLine(item);
                }
            }

            return JsonConvert.SerializeObject(table);
        }
    }
}
