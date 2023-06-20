using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;

namespace surveyBackend.Pages.Questions
{
    public class CreateModel : PageModel
    {
        public QuestionInfo questionInfo = new QuestionInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            questionInfo.qKey = Request.Form["qKey"];
            questionInfo.qLabel = Request.Form["qLabel"];
            questionInfo.multiple_choice = Request.Form["multiple_choice"] == "on";

            if (questionInfo.qKey.Length == 0 || questionInfo.qLabel.Length == 0 )
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=DYLANPAYNE;Initial Catalog=survey;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection( connectionString ))
                {
                    connection.Open();
                    String sql = "INSERT INTO questions " +
                                 "(qKey, qLabel, multiple_choice) VALUES " +
                                 "(@qKey, @qLabel, @multiple_choice)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@qKey", questionInfo.qKey);
                        command.Parameters.AddWithValue("@qLabel", questionInfo.qLabel);
                        command.Parameters.AddWithValue("@multiple_choice", questionInfo.multiple_choice);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch ( Exception ex )
            {
                errorMessage = ex.Message;
                return;
            }

            questionInfo.qKey = ""; questionInfo.qLabel = "";
            successMessage = "New question added successfully";

            Response.Redirect("/Questions/Index");
        }
    }
}
