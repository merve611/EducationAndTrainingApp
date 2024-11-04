using EducationAndTrainingApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Business.Operations.Course.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public LevelType LevelType { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public int StudentNumber { get; set; }
        public List<CourseLessonDto> Lessons { get; set; }
    }
}
