namespace WebAPI.Dto
{
    public class NewQuizItemDto
    {
            public string Question { get; set; }
            public List<string> IncorrectAnswers { get; set; }
            public string CorrectAnswer { get; set; }
    
    }
}
