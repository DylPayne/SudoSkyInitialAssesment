using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

public class Questions
{
    public int id { get; set; }
    public string key { get; set; }
    public string label { get; set; }
    public List<Options> options { get; set; }
    public bool multiple_choice { get; set; }
}
public class Options
{
    public string key { get; set; }
    public string label { get; set; }
    public int qId { get; set; }
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
            SqlConnection connection = new SqlConnection(Configuration["ConnectionStrings:DevConnection"] + ";MultipleActiveResultSets=true");
            SqlCommand command = new SqlCommand("SELECT id, qKey, qLabel, multiple_choice FROM questions", connection);
            SqlCommand commandOption = new SqlCommand("SELECT oKey, oLabel, qId FROM questionOptions", connection);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            SqlDataAdapter adapterOption = new SqlDataAdapter(commandOption);
            DataTable table = new DataTable();
            DataTable tableOption = new DataTable();
            adapter.Fill(table);
            adapterOption.Fill(tableOption);
            connection.Close();

            // TODO: fix this. The column names are not being set correctly because I am not using the Question class properly. This is a hack
            table.Columns["qKey"].ColumnName = "key";
            table.Columns["qLabel"].ColumnName = "label";

            table.Columns.Add("options", typeof(List<Options>));

            String questionsJson = JsonConvert.SerializeObject(table);
            String optionsJson = JsonConvert.SerializeObject(tableOption);

            List<Questions> questions = new List<Questions>();
            List<Options> options = new List<Options>();

            questions = JsonConvert.DeserializeObject<List<Questions>>(questionsJson);
            options = JsonConvert.DeserializeObject<List<Options>>(optionsJson);

            Console.WriteLine("Questions: ");
            Console.WriteLine(questionsJson);

            Console.WriteLine("Options: ");
            Console.WriteLine(optionsJson);

            foreach (DataRow dataRowQuestion in table.Rows)
            {
                var questionId = dataRowQuestion.ItemArray[0];
                var i = 0;
                List<Options> tempRow = new List<Options>();
                foreach (DataRow dataRowOption in tableOption.Rows)
                {
                    if (questionId.ToString() == dataRowOption.ItemArray[2].ToString())
                    {
                        tempRow.Add(new Options
                        {
                            key = dataRowOption.ItemArray[0].ToString(),
                            label = dataRowOption.ItemArray[1].ToString(),
                            qId = Convert.ToInt32(dataRowOption.ItemArray[2])
                        });
                        dataRowQuestion.SetField("options", tempRow);
                    }
                }
                Console.WriteLine(JsonConvert.SerializeObject(tempRow));
            }

            Console.WriteLine(JsonConvert.SerializeObject(table));

            return JsonConvert.SerializeObject(table);
        }
    }
}
