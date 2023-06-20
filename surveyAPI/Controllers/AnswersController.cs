using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Net.Security;
using System.ComponentModel.Design;

namespace surveyAPI.Controllers
{

    public class Answer
    {
        public int ID { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : Controller
    {
        [HttpGet]
        public string GetAllAnswers()
        {
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=survey;Integrated Security=True");
            SqlCommand command = new SqlCommand("SELECT oKey, oLabel, selections, qId FROM questionOptions", connection);
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connection.Close();
            return JsonConvert.SerializeObject(table);
        }

        [HttpPost]
        public string PostAnswers([FromBody] String[] answers)
        {
            SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=survey;Integrated Security=True;MultipleActiveResultSets=true");
            connection.Open();
            foreach (string answer in answers)
            {
                SqlCommand command = new SqlCommand("SELECT selections FROM questionOptions WHERE oKey=@value", connection);
                command.Parameters.AddWithValue("@value", answer);
                SqlDataReader rd = command.ExecuteReader();
                var selections = 0;
                if (rd.HasRows)
                {
                    rd.Read();
                    selections = rd.GetInt32(0) + 1;
                }
                SqlCommand command2 = new SqlCommand("UPDATE questionOptions SET selections=@selections WHERE oKey=@value", connection);
                command2.Parameters.AddWithValue("@selections", selections);
                command2.Parameters.AddWithValue("@value", answer);
                command2.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
        }
    }
}
