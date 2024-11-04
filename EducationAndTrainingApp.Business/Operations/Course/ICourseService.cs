using EducationAndTrainingApp.Business.Operations.Course.Dtos;
using EducationAndTrainingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Business.Operations.Course
{
    public interface ICourseService
    {
        Task<ServiceMessage> AddCourse(AddCourseDto course);
        Task<CourseDto> GetCourse(int id);
        Task<List<CourseDto>> GetAllCourses();
        Task<ServiceMessage> ChangeStudentNumber(int id,int changeTo);
        Task<ServiceMessage> DeleteCourse(int id);
        Task<ServiceMessage> UpdateCourse(UpdateCourseDto course);
    }
}
