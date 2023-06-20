using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace surveyBackend.Pages.Questions
{
    public class IndexModel : PageModel
    {
        public List<QuestionInfo> listQuestions = new List<QuestionInfo>();
        public void OnGet()
        {
            try
            {
                String connectingString = "Data Source=DYLANPAYNE;Initial Catalog=survey;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectingString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM questions";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                QuestionInfo questionInfo = new QuestionInfo();
                                questionInfo.id = "" + reader.GetInt32(0);
                                questionInfo.qKey = reader.GetString(1);
                                questionInfo.qLabel = reader.GetString(2);
                                questionInfo.created_at = reader.GetDateTime(3).ToString();
                                questionInfo.multiple_choice = reader.GetBoolean(4);

                                listQuestions.Add(questionInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class QuestionInfo
    {
        public string id { get; set; }
        public string qKey { get; set; }
        public string qLabel { get; set; }
        public string created_at { get; set; }
        public bool multiple_choice { get; set; }
    }
}
