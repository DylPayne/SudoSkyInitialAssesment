using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using surveyBackend.Pages.Questions;
using System.Data.SqlClient;

namespace surveyBackend.Pages.QuestionOptions
{
    public class EditModel : PageModel
    {
        public QuestionOptionInfo questionOptionInfo = new QuestionOptionInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public List<QuestionInfo> listQuestions = new List<QuestionInfo>();
        public void OnGet()
        {
            String id = Request.Query["id"];


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

            try
            {
                String connectionString = "Data Source=DYLANPAYNE;Initial Catalog=survey;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM questionOptions WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                questionOptionInfo.id = "" + reader.GetInt32(0);
                                questionOptionInfo.oKey = reader.GetString(1);
                                questionOptionInfo.oLabel = reader.GetString(2);
                                questionOptionInfo.qId = "" + reader.GetInt32(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            questionOptionInfo.id = Request.Query["id"];
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
                    String sql = "UPDATE questionOptions SET oKey=@oKey, oLabel=@oLabel, qId=@qId WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", questionOptionInfo.id);
                        command.Parameters.AddWithValue("@oKey", questionOptionInfo.oKey);
                        command.Parameters.AddWithValue("@oLabel", questionOptionInfo.oLabel);
                        command.Parameters.AddWithValue("@qId", questionOptionInfo.qId);
                        command.ExecuteNonQuery();
                    }
                }
                successMessage = "Question option updated successfully";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            Response.Redirect("/QuestionOptions/Index");
        }
    }
}
