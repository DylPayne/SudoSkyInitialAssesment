using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

public class Questions
{
    public string key { get; set; }
    public string label { get; set; }
    public List<Options> options { get; set; }
}
public class Options
{
    public string key { get; set; }
    public string label { get; set; }
    public int question_id { get; set; }
}

namespace surveyAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class QuestionOptionsController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        public QuestionOptionsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public string GetAllQuestionOptions()
        {
            SqlConnection connection = new SqlConnection(Configuration["ConnectionStrings:DevConnection"]);
            SqlCommand command = new SqlCommand("SELECT * FROM questions", connection);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();

            foreach (DataRow dataRow in table.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    Console.WriteLine(item);
                }
            }

            return JsonConvert.SerializeObject(table);
        }
    }
}
