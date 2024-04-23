using FluentValidation;
using WebAPI.Dto;

namespace WebAPI.Validators
{
    public class NewQuizItemDtoValidator : AbstractValidator<NewQuizItemDto>
    {
        public NewQuizItemDtoValidator()
        {
            RuleFor(dto => dto.Question)
                        .NotEmpty().WithMessage("Question is required.")
                        .MaximumLength(200).WithMessage("Question cannot be longer than 200 characters.")
                        .MinimumLength(3).WithMessage("Question must be at least 3 characters long.");

            RuleForEach(dto => dto.IncorrectAnswers)
                .NotEmpty().WithMessage("Incorrect answer cannot be empty.")
                .MaximumLength(200).WithMessage("Incorrect answer cannot be longer than 200 characters.");

            RuleFor(dto => dto.CorrectAnswer)
                .NotEmpty().WithMessage("Correct answer is required.")
                .MaximumLength(200).WithMessage("Correct answer cannot be longer than 200 characters.");
        }
    }
}
