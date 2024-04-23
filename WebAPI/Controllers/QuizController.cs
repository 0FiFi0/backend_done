using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("/api/v1/quizzes")]
public class QuizController : ControllerBase
{
    private readonly IQuizUserService _service;
    private readonly IMapper _mapper;

    public QuizController(IQuizUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    [HttpGet]
    [Route("{id}")]
    public ActionResult<QuizDto> FindById(int id)
    {
        var result = QuizDto.of(_service.FindQuizById(id));
        return result is null ? NotFound() : Ok(_mapper.Map<QuizDto>(result));
    }

    [HttpGet]
    public IEnumerable<QuizDto> FindAll()
    {
        return _service.FindAllQuizzes().Select(QuizDto.of).AsEnumerable();
    }

    [HttpPost]
    [Authorize(Policy = "Bearer")]
    [Route("{quizId}/items/{itemId}/answers")]
    public ActionResult SaveAnswer([FromBody] QuizItemAnswerDto dto, int quizId, int itemId)
    {
        try
        {
            var answer = _service.SaveUserAnswerForQuiz(quizId, itemId, dto.UserId, dto.Answer);
            return Created("", answer);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //[HttpPost]
    //[Route("{quizId}/items/{itemId}/answers")]
    //public ActionResult SaveAnswer([FromBody] QuizItemAnswerDto dto, int quizId, int itemId)
    //{
    //    try
    //    {
    //        var answer = _service.SaveUserAnswerForQuiz(quizId, itemId, dto.UserId, dto.Answer);
    //        return Created("", answer);
    //    }
    //    catch (Exception)
    //    {
    //        return BadRequest();
    //    }
    //}

    [HttpGet]
    [Route("{quizId}/answers")]
    public ActionResult<object> CountCorrectAnswers(int quizId)
    {
        int count = _service.CountCorrectAnswersForQuizFilledByUser(quizId, 1);//1- id uzytkownika 
        return new
        {
            ValidAnswers = count,
            QuizId = quizId,
            UserId = 1
        };
    }


    // // // Quiz// // // // // // // // // // // // // // 

    [Route("{quizId}/answers/{userId}")]
    [HttpGet]
    public ActionResult<object> GetQuizFeedback(int quizId, int userId)
    {
        var feedback = _service.GetUserAnswersForQuiz(quizId, userId);
        return new
        {
            quizId = quizId,
            userId = userId,
            totalQuestions = _service.FindQuizById(quizId)?.Items.Count ?? 0,
            answers = feedback.Select(a =>
                new
                {
                    question = a.QuizItem.Question,
                    answer = a.Answer,
                    isCorrect = a.IsCorrect()
                }
            ).AsEnumerable()
        };
    }


}
