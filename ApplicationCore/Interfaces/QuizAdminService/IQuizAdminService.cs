using ApplicationCore.Models;

namespace ApplicationCore.Interfaces;

public interface IQuizAdminService
{
    public QuizItem AddQuizItem(string question, List<string> incorrectAnswers, string correctAnswer, int points);
    public void UpdateQuizItem(int id, string question, List<string> incorrectAnswers, string correctAnswer, int points);
    public Quiz AddQuiz(Quiz quiz);
    public QuizItem AddQuizItemToQuiz(int quizId, QuizItem item);
    public List<QuizItem> FindAllQuizItems();
    public List<Quiz> FindAllQuizzes();
}