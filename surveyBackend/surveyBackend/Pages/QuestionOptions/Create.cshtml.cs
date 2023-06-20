using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using surveyBackend.Pages.Questions;
using System.Data.SqlClient;

namespace surveyBackend.Pages.QuestionOptions
{
    public class CreateModel : PageModel
    {
        public QuestionOptionInfo questionOptionInfo = new QuestionOptionInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public List<QuestionInfo> listQuestions = new List<QuestionInfo>();
        public void OnGet ()
        {
            try
            {
                String connectingString = "Data Source=DYLANPAYNE;Initial Catalog=survey;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectingString))
                {
                    connection.Open();
                    String sql = "SELECT id, qKey FROM questions";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                QuestionInfo questionInfo = new QuestionInfo();

                                questionInfo.id = "" + reader.GetInt32(0);
                                questionInfo.qKey = reader.GetString(1);

                                listQuestions.Add(questionInfo);
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

        public void OnPost()
        {
            questionOptionInfo.oKey = Request.Form["oKey"];
            questionOptionInfo.oLabel = Request.Form["oLabel"];
            questionOptionInfo.qId = Request.Form["qId"];

            if (questionOptionInfo.oKey.Length == 0 || questionOptionInfo.oLabel.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=DYLANPAYNE;Initial Catalog=survey;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO questionOptions " +
                                 "(oKey, oLabel, qId) VALUES " +
                                 "(@oKey, @oLabel, @qId)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@oKey", questionOptionInfo.oKey);
                        command.Parameters.AddWithValue("@oLabel", questionOptionInfo.oLabel);
                        command.Parameters.AddWithValue("@qId", questionOptionInfo.qId);

                        command.ExecuteNonQuery();
                    }
                }

                questionOptionInfo.oKey = ""; questionOptionInfo.oLabel = ""; questionOptionInfo.qId = "";
                successMessage = "New question option added successfully";

                Response.Redirect("/QuestionOptions/Index");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
    }
}
