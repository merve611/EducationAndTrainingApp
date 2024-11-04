using System.ComponentModel.DataAnnotations;

namespace EducationAndTrainingApp.WebApi.Models
{
    public class AddLessonRequest
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
    }
}
