using EducationAndTrainingApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace EducationAndTrainingApp.WebApi.Models
{
    public class UpdateHotelRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        public LevelType LevelType { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Duration { get; set; }

        [Range(10, 50, ErrorMessage = "Kurstaki öğrenci sayısı min 10 max 50 olabilir ")]
        public int StudentNumber { get; set; }
        public List<int> LessonIds { get; set; }
    }
}
