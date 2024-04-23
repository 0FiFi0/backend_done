namespace WebAPI.Controllers;

public class FeedbackQuizDto
{ 
   public int QuizId { get; init; }
   
   public int UserId { get; set; }

    public int TotalQuestions { get; set; }

    public IEnumerable<FeedbackQuizItemDto> Answers { get; set; }
}