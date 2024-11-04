using EducationAndTrainingApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Business.Operations.Course.Dtos
{
    public class AddCourseDto
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public LevelType LevelType { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public int StudentNumber { get; set; }
        public List<int> LessonIds { get; set; }
    }
}
