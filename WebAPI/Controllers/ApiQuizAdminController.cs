using System.Collections;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI.Controllers
{
    public class ApiQuizAdminController: ControllerBase
    {
        private readonly IQuizAdminService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<QuizItem> _validator;

        public ApiQuizAdminController(IQuizAdminService service, IMapper mapper, IValidator<QuizItem> validator)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
        }

        // [HttpPost]
        // public ActionResult<object> AddQuiz(LinkGenerator link, NewQuizDto dto)
        // {
        //     var quiz = _service.AddQuiz(new Quiz() { Title = dto.Title });
        //     return Created(
        //         link.GetPathByAction(
        //             HttpContext,
        //             nameof(GetQuiz),         // nazwa metody kontrolera zwracająca quiz
        //             null,                    // kontroler, null oznacza bieżący
        //             new { quiId = quiz.Id }),// parametry ścieżki, id utworzonego quiz
        //         quiz
        //     );
        // }
        [HttpPost]
        public IActionResult CreateQuiz(LinkGenerator link, NewQuizDto dto)
        {
            var quiz = _service.AddQuiz(new Quiz() { Title = dto.Title });

            return Created(
                link.GetUriByAction(HttpContext, nameof(GetQuiz), null, new { quizId = quiz }),
                quiz
                );
        }

        [HttpGet]
        [Route("{quizId}")]
        public ActionResult<Quiz> GetQuiz(int quizId)
        {
            var quiz = _service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);
            return quiz is null ? NotFound() : Ok(quiz);
        }

        [HttpPatch]
        [Route("{quizId}")]
        [Consumes("application/json-patch+json")]
        public ActionResult<Quiz> AddQuizItem(int quizId, JsonPatchDocument<Quiz>? patchDoc)
        {
            var quiz = _service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId);
            if (quiz is null || patchDoc is null)
            {
                return NotFound(new
                {
                    error = $"Quiz width id {quizId} not found"
                });
            }
            int previousCount = quiz.Items.Count;
            patchDoc.ApplyTo(quiz, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (previousCount < quiz.Items.Count)
            {
                QuizItem item = quiz.Items[^1];
                
                var validationResult = _validator.Validate(item);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                
                quiz.Items.RemoveAt(quiz.Items.Count - 1);
                _service.AddQuizItemToQuiz(quizId, item);
            }
            return Ok(_service.FindAllQuizzes().FirstOrDefault(q => q.Id == quizId));
        }

        [HttpPost]
        [Route("/items/{quizId}")]
        
        public IActionResult TestValidation(int quizId, NewQuizItemDto dto)
        {
            var temp=new QuizItem(){                
                Id=_service.FindAllQuizItems().Count+1,
                Question= dto.Question,
                CorrectAnswer = dto.CorrectAnswer,
                IncorrectAnswers =dto.IncorrectAnswers
            };
            var validationResult = _validator.Validate(temp);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return Ok("Dane są poprawne.");
        }
    }
}
