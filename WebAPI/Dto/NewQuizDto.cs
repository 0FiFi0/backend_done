using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto
{
    public class NewQuizDto
    {
        [Required]
        [MaxLength(200), MinLength(3)]
        //[Length(minimumLength: 3, maximumLength: 200)]
        public string Title { get; set; }
    }
}
