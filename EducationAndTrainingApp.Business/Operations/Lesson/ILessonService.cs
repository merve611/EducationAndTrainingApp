using EducationAndTrainingApp.Business.Operations.Lesson.Dtos;
using EducationAndTrainingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Business.Operations.Lesson
{
    public interface ILessonService 
    {
        Task<ServiceMessage> AddLesson(AddLessonDto lesson);
    }
}
