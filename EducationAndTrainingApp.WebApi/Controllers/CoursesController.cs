using EducationAndTrainingApp.Business.Operations.Course;
using EducationAndTrainingApp.Business.Operations.Course.Dtos;
using EducationAndTrainingApp.Business.Operations.Lesson.Dtos;
using EducationAndTrainingApp.WebApi.Filters;
using EducationAndTrainingApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationAndTrainingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // id ile tek bir kursun bilgilerini getiren endpoint
        [HttpGet("id")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourse(id);
            
            if(course is null)
                return NotFound();
            else
                return Ok(course);
        }
        // tüm kursları getiren endpoint
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllCourses();

            return Ok(courses);
        }



        [HttpPost("course-added")]
        [Authorize(Roles ="Instructor,Admin")]        //Yanlızca eğitmenler ve admin kurs ekleyebilir
        public async Task<IActionResult> AddCourse(AddCourseRequest request)
        {
            //request ten gelen bilgiler AddLessonDto ya çevrilir

            var addCourseDto = new AddCourseDto
            {
                UserId = request.UserId,
                Description = request.Description,
                LevelType = request.LevelType,
                Price = request.Price,
                Duration = request.Duration,
                StudentNumber = request.StudentNumber,
                LessonIds = request.LessonIds,
            };


            var result = await _courseService.AddCourse(addCourseDto);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Message);
        }


        [HttpPatch("{id}/StudentNumber")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStudentNumber(int id, int changeTo)
        {
            var result = await _courseService.ChangeStudentNumber(id, changeTo);

            if (!result.IsSucced)
                return NotFound(result.Message);
            else
                return Ok();


        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourse(id);

            if (!result.IsSucced)
                return NotFound(result.Message);
            else
                return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [DayAndTimeControlFilter]

        public async Task<IActionResult> UpdateCourse(int id, UpdateHotelRequest request)
        {
            var updateCourseDto = new UpdateCourseDto
            {
                Id = id,
                UserId = request.UserId,
                Description = request.Description,
                LevelType = request.LevelType,
                Price = request.Price,
                Duration = request.Duration,
                StudentNumber = request.StudentNumber,
                LessonIds = request.LessonIds,
            };

            var result = await _courseService.UpdateCourse(updateCourseDto);

            if (!result.IsSucced)
                return NotFound(result.Message);
            else
                return await GetCourse(id);


        }
    }
}
