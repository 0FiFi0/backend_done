using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic; // Include this namespace for IEnumerable<T>
using Web.Pages;

namespace WseiBackendLab.Pages.Quiz
{
    public class QuizzesModel : PageModel
    {
        private readonly IQuizUserService _userService;
        private readonly ILogger _logger;

        public QuizzesModel(IQuizUserService userService, ILogger<QuizModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public IEnumerable<ApplicationCore.Models.Quiz> lista; // Change the type to IEnumerable<Quiz>

        public void OnGet()
        {
            lista = _userService.FindAllQuizzes();
        }
    }
}
