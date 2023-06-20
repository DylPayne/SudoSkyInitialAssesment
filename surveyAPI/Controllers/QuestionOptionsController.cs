using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace surveyAPI.Controllers
{
    public class Option
    {
        public int id { get; set; }
        public string key { get; set; }
        public string label { get; set; }
        public int qKey { get; set; }
    }
    public class Question
    {
        public int id { get; set; }
        public string key { get; set; }
        public string label { get; set; }
        public string created_at { get; set; }
        public bool multiple_choice { get; set; }
    }
    public class QuestionsWithOptions
    {
        public string key { get; set; }
        public string label { get; set; }
        public List<Option> options { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class QuestionOptionsController : ControllerBase
    {
        SqlConnection connection = new SqlConnection("Data Source=DYLANPAYNE;Initial Catalog=survey;Integrated Security=True");

        [HttpGet]
        public string GetAllQuestionOptions()
        {
            var questionsWithOptions = new List<QuestionsWithOptions>();

            SqlCommand optionsCommand = new SqlCommand("SELECT * FROM questionOptions", connection);
            SqlCommand questionsCommand = new SqlCommand("SELECT * FROM questionOptions", connection);

            connection.Open();

            SqlDataAdapter optionsAdapter = new SqlDataAdapter(optionsCommand);
            SqlDataAdapter questionsAdapter = new SqlDataAdapter(questionsCommand);

            DataTable optionsTable = new DataTable();
            DataTable questionsTable = new DataTable();

            optionsAdapter.Fill(optionsTable);
            optionsAdapter.Fill(questionsTable);

            connection.Close();

            //string v = JsonConvert.SerializeObject(questionsTable);
            //List<Question> questionList = v;

            //foreach (Question question in JsonConvert.SerializeObject(questionsTable))
            //{
            //    var questionWithOptions = new QuestionsWithOptions();
            //    questionWithOptions.key = question.key;
            //    questionWithOptions.label = question.label;
            //    questionWithOptions.options = new List<Option>();

            //    foreach (Option option in JsonConvert.SerializeObject(optionsTable))
            //    {
            //        if (option.qKey == question.key)
            //        {
            //            var newOption = new Option();
            //            newOption.key = option.key;
            //            newOption.label = option.label;
            //            questionWithOptions.options.Add(newOption);
            //        }
            //    }

            //    questionsWithOptions.Add(questionWithOptions);
            //}   

            return JsonConvert.SerializeObject(optionsTable);
        }
    }
}
