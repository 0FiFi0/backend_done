using ApplicationCore.Models;

namespace WebAPI.Controllers;

public class QuizItemDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public List<string> Options { get; set; }

    public static QuizItemDto of(QuizItem item)
    {
        if (item is null)
        {
            return null;
        }
        List<string> options = new List<string>();
        options.Add(item.CorrectAnswer);
        options.AddRange(item.IncorrectAnswers);
        Random rand = new Random();
        options.Sort(comparison:(a,b)=>1-rand.Next(maxValue:3));
        return new QuizItemDto()
        {
            Id = item.Id,
            Question = item.Question,
            Options = options
        };
    }
}