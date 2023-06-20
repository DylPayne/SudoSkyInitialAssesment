using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace surveyBackend.Pages.QuestionOptions
{
    public class IndexModel : PageModel
    {
        public List<QuestionOptionInfo> listQuestionOptions = new List<QuestionOptionInfo>();
        public void OnGet()
        {
            try
            {
                String connectingString = "Data Source=DYLANPAYNE;Initial Catalog=survey;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectingString))
                {
                    connection.Open();
                    String sql = "SELECT questionOptions.id, questionOptions.oKey, questionOptions.oLabel, questions.qKey FROM questionOptions " +
                                 "INNER JOIN questions ON questionOptions.qId = questions.id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                QuestionOptionInfo questionOptionInfo = new QuestionOptionInfo();

                                questionOptionInfo.id = "" + reader.GetInt32(0);
                                questionOptionInfo.oKey = reader.GetString(1);
                                questionOptionInfo.oLabel = reader.GetString(2);
                                questionOptionInfo.qKey = reader.GetString(3);

                                listQuestionOptions.Add(questionOptionInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }

    public class QuestionOptionInfo
    {
        public string id { get; set; }
        public string oKey { get; set; }
        public string oLabel { get; set; }
        public string qKey { get; set; }
        public string qId { get; set; }
        public string created_at { get; set; }
    }
}
