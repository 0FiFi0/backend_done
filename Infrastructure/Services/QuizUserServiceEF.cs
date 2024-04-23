using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;

using ApplicationCore.Models;
using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class QuizUserServiceEF : IQuizUserService
{
    private QuizDbContext _service;

    public QuizUserServiceEF(QuizDbContext service)
    {
        _service = service;
    }
    public Quiz CreateAndGetQuizRandom(int count)
    {
        throw new NotImplementedException();
    }

    public Quiz? FindQuizById(int id)
    {
        var entity = _service
            .Quizzes
            .AsNoTracking()
            .Include(q => q.Items)
            .ThenInclude(i => i.IncorrectAnswers)
            .FirstOrDefault(e => e.Id == id);
        return entity is null ? null : Mapper.FromEntityToQuiz(entity);
    }
    public IEnumerable<Quiz> FindAllQuizzes()
    {
        return _service
            .Quizzes
            .AsNoTracking()
            .Include(q => q.Items)
            .ThenInclude(i => i.IncorrectAnswers)
            .Select(Mapper.FromEntityToQuiz)
            .ToList();
    }
    //public QuizItem GetQuizItemById(int id)
    //{
    //    var item = _service.QuizItems
    //        .AsNoTracking()
    //        .FirstOrDefault(i => i.Id == id);
    //    return Mapper.FromEntityToQuizItem(item);
    //}

    public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer)
    {
        var quizzEntity = FindQuizById(quizId); // pobierz encję quizu o quizId
        //if (quizzEntity is null)
        //{
        //    throw new QuizNotFoundException($"Quiz with id {quizId} not found");
        //}

        var items = quizzEntity.Items; // pobierz encję elementu quizu o quizItemId
        QuizItem item = null;
        foreach (var x in items)
        {
            if (x.Id == quizItemId) item = x;
        }

        //if (item is null)
        //{
        //    throw new QuizItemNotFoundException($"Quiz item with id {quizId} not found");
        //}
        QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
        {
            QuizId = quizId,
            UserAnswer = answer,
            UserId = userId,
            QuizItemId = quizItemId
        };
        try
        {
            _service.UserAnswers.Add(entity);
            _service.SaveChanges();
            return new QuizItemUserAnswer(
                item, quizId, userId, answer);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException.Message.StartsWith("The INSERT"))
            {
                throw new QuizNotFoundException("Quiz, quiz item or user not found. Can't save!");
            }
            if (e.InnerException.Message.StartsWith("Violation of"))
            {
                throw new QuizAnswerItemAlreadyExistsException(quizId, quizItemId, userId);
            }
            throw new Exception(e.Message);
        }
        // uzupełnij aby zmapować obiekt entity na klasę QuizItemUserAnswer 

    }

    public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
    {
        var entity = _service.UserAnswers
       .Where(ua => ua.QuizId == quizId && ua.UserId == userId)
       .Select(ua => new QuizItemUserAnswer(
            Mapper.FromEntityToQuizItem(ua.QuizItem),
            ua.QuizId,
            ua.UserId,
            ua.UserAnswer)).ToList();

        //Select(ua => Mapper.FromEntityToItemUserAnswer(ua)).ToList();

        return entity;
    }

}