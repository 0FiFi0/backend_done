using ApplicationCore.Models;
using AutoMapper;
using WebAPI.Controllers;
using WebAPI.Dto;

namespace WebAPI.Mappers
{
    public class AutoMapperProfiles: Profile
    {
    
        public AutoMapperProfiles()
        {
            CreateMap<QuizItem, QuizItemDto>()
                .ForMember(
                    q => q.Options,
                    op => op.MapFrom(i => new List<string>(i.IncorrectAnswers) { i.CorrectAnswer }));
            CreateMap<Quiz, QuizDto>()
                .ForMember(
                    q => q.Items,
                    op => op.MapFrom<List<QuizItem>>(i => i.Items)
                );
            CreateMap<NewQuizDto, Quiz>();
            
            CreateMap<QuizItemUserAnswer, FeedbackQuizItemDto>()
            .ForMember(dest 
                => dest.Question, opt 
                => opt.MapFrom(src => src.QuizItem.Question))
            .ForMember(dest 
                => dest.Answer, opt 
                => opt.MapFrom(src => src.Answer))
            .ForMember(dest 
                => dest.IsCorrect, opt 
                => opt.MapFrom(src => src.IsCorrect()));


            CreateMap<(int QuizId, int UserId, int TotalQuestions, IEnumerable<QuizItemUserAnswer> Feedback), FeedbackQuizDto>()
            .ForMember(dest 
                => dest.QuizId, opt 
                => opt.MapFrom(src => src.QuizId))
            .ForMember(dest 
                => dest.UserId, opt 
                => opt.MapFrom(src => src.UserId))
            .ForMember(dest 
                => dest.TotalQuestions, opt 
                => opt.MapFrom(src => src.TotalQuestions))
            .ForMember(dest 
                => dest.Answers, opt 
                => opt.MapFrom(src => src.Feedback));

        }
    }
}