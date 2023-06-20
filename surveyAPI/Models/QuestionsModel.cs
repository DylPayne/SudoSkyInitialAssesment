namespace surveyAPI.Models
{
    public class QuestionsModel
    {
        public int id { get; set; }
        public string qKey { get; set; }
        public string qLabel { get; set; }
        public DateTime created_at { get; set; }
        public bool multiple_choice { get; set; }
    }
}
