using ApplicationCore.Models;
using Infrastructure.Entities;

namespace Infrastructure.Mappers;

public static class Mapper
{
    public static QuizItem FromEntityToQuizItem(QuizItemEntity entity)
    {
        return new QuizItem(
            entity.Id,
            entity.Question,
            entity.IncorrectAnswers.Select(e => e.Answer).ToList(),
            entity.CorrectAnswer);
    }

    public static Quiz FromEntityToQuiz(QuizEntity entity)
    {
        List<QuizItem> tempList = new List<QuizItem>();

        foreach (var item in entity.Items)
        {
            tempList.Add(FromEntityToQuizItem(item));
        }

        return new Quiz(){
            Id = entity.Id,
            Title = entity.Title,
            Items = tempList
            };
    }
    public static QuizItemUserAnswer FromEntityToItemUserAnswer(QuizItemUserAnswerEntity answer)
    {
        return new QuizItemUserAnswer(FromEntityToQuizItem(answer.QuizItem),answer.UserId,answer.QuizId,answer.UserAnswer);
    }
   

}