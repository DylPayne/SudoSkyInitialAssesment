using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace surveyBackend.Pages.Questions
{
    public class EditModel : PageModel
    {

        public QuestionInfo questionInfo = new QuestionInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=DYLANPAYNE;Initial Catalog=survey;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM questions WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                questionInfo.id = "" + reader.GetInt32(0);
                                questionInfo.qKey = reader.GetString(1);
                                questionInfo.qLabel = reader.GetString(2);
                                questionInfo.multiple_choice = reader.GetBoolean(4);
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
            questionInfo.id = Request.Form["id"];
            questionInfo.qKey = Request.Form["qKey"];
            questionInfo.qLabel = Request.Form["qLabel"];
            questionInfo.multiple_choice = Request.Form["multiple_choice"] == "on";

            if (questionInfo.id.Length == 0 || questionInfo.qKey.Length == 0 || questionInfo.qLabel.Length == 0)
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
                    String sql = "UPDATE questions " +
                                 "SET qKey=@qKey, qLabel=@qLabel, multiple_choice=@multiple_choice " +
                                 "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", questionInfo.id);
                        command.Parameters.AddWithValue("@qKey", questionInfo.qKey);
                        command.Parameters.AddWithValue("@qLabel", questionInfo.qLabel);
                        command.Parameters.AddWithValue("@multiple_choice", questionInfo.multiple_choice);

                        command.ExecuteNonQuery();
                    }               
                }
            } catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Questions/Index");
        }
    }
}
