using EducationAndTrainingApp.Business.Operations.Lesson.Dtos;
using EducationAndTrainingApp.Business.Types;
using EducationAndTrainingApp.Data.Entities;
using EducationAndTrainingApp.Data.Repositories;
using EducationAndTrainingApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Business.Operations.Lesson
{
    public class LessonManager : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<LessonEntity> _repository;

        public LessonManager(IUnitOfWork unitOfWork, IRepository<LessonEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<ServiceMessage> AddLesson(AddLessonDto lesson)
        {
            var hasLesson = _repository.GetAll(x => x.Title.ToLower() == lesson.Title.ToLower()).Any();


            if (hasLesson)
            {

                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "Girilen ders zaten var"
                };
            }

            var lessonEntity = new LessonEntity
            {
                Title = lesson.Title,
            };

            _repository.Add(lessonEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Ders kaydı sırasında bir hata oluştu");
            }

            return new ServiceMessage
            {
                IsSucced = true
            };




        }
}
}
