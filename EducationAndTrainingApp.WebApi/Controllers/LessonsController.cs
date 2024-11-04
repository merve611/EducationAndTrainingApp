using EducationAndTrainingApp.Business.Operations.Lesson;
using EducationAndTrainingApp.Business.Operations.Lesson.Dtos;
using EducationAndTrainingApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationAndTrainingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin,Instructor")]            //Yanlızca adminler ve eğitmenler ders ekleyebilir
        public async Task<IActionResult> AddLesson(AddLessonRequest request)
        {
            var addLessonDto = new AddLessonDto
            {
                Title = request.Title,
            };


            var result = await _lessonService.AddLesson(addLessonDto);

            if (result.IsSucced)
                return Ok();
            else
                return BadRequest(result.Message);

        }

    }
}
