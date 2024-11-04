using EducationAndTrainingApp.Business.Operations.Course.Dtos;
using EducationAndTrainingApp.Business.Types;
using EducationAndTrainingApp.Data.Entities;
using EducationAndTrainingApp.Data.Repositories;
using EducationAndTrainingApp.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Business.Operations.Course
{
    public class CourseManager : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<CourseEntity> _courseRepository;
        private readonly IRepository<CourseLessonEntity> _courseLessonRepository;

        public CourseManager(IUnitOfWork unitOfWork, IRepository<CourseEntity> courseRepository, IRepository<CourseLessonEntity> courseLessonRepository)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _courseLessonRepository = courseLessonRepository;
        }


        public async Task<ServiceMessage> AddCourse(AddCourseDto course)
        {
            var hasCourse = _courseRepository.GetAll(x => x.Description.ToLower() == course.Description.ToLower()).Any();

            if(hasCourse)
            {
                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "Girilen Kurs zaten var"
                };
            }

            await _unitOfWork.BeginTransaction();


            var courseEntity = new CourseEntity
            {
                UserId  = course.UserId,
                Description = course.Description,
                LevelType = course.LevelType,
                Price = course.Price,
                Duration = course.Duration,
                StudentNumber = course.StudentNumber,
                
            };

            _courseRepository.Add(courseEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kurs kaydı sırasında bir hata oluştu");
            }

            foreach (var lessonId in course.LessonIds)
            {
                var courseLesson = new CourseLessonEntity
                {
                    CourseId = courseEntity.Id,
                    LessonId = lessonId,
                };

                _courseLessonRepository.Add(courseLesson);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Kurs dersleri eklenirken bir hatayla karşılaşıldı, süreç başa sardı ");
            }




            return new ServiceMessage
            {
                IsSucced = true
            };
        }
        public async Task<CourseDto> GetCourse(int id)
        {
            var course = await _courseRepository.GetAll(x => x.Id == id)
                .Select(x => new CourseDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    LevelType = x.LevelType,
                    Price = x.Price,
                    Duration = x.Duration,
                    StudentNumber = x.StudentNumber,
                    Lessons = x.CourseLessons.Select(l => new CourseLessonDto
                    {
                        Id = l.Id,
                        Title = l.Lesson.Title
                    }).ToList()
                }).FirstOrDefaultAsync();


            return course;
        }

        public async Task<List<CourseDto>> GetAllCourses()
        {
            var courses = await _courseRepository.GetAll()
                .Select(x => new CourseDto
                {
                    Id = x.Id,
                    Description = x.Description,
                    LevelType = x.LevelType,
                    Price = x.Price,
                    Duration = x.Duration,
                    StudentNumber = x.StudentNumber,
                    Lessons = x.CourseLessons.Select(l => new CourseLessonDto
                    {
                        Id = l.Id,
                        Title = l.Lesson.Title
                    }).ToList()

                }).ToListAsync();

            return courses;
        }

        public async Task<ServiceMessage> ChangeStudentNumber(int id, int changeTo)
        {
           var course = _courseRepository.GetById(id);

            if(course is null)
            {
                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "İlgili id li kurs bulunamadı"
                };

            }

            course.StudentNumber = changeTo;
            _courseRepository.Update(course);

            try
            {
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw new Exception("Öğrenci sayısı değiştirilirken bir hata oluşru");
            }

            return new ServiceMessage
            {
                IsSucced = true
            };
        }

        public async Task<ServiceMessage> DeleteCourse(int id)
        {
            var course = _courseRepository.GetById(id);

            if (course is null)
            {
                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "İlgili id li kurs bulunamadı"
                };

            }

            _courseRepository.Delete(id);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kurs silinirken bir hata oluştu");
            }

            return new ServiceMessage
            {
                IsSucced = true
            };

        }

        public async Task<ServiceMessage> UpdateCourse(UpdateCourseDto course)
        {
            var courseEntity = _courseRepository.GetById(course.Id);

            if (courseEntity is null)
            {
                return new ServiceMessage
                {
                    IsSucced = false,
                    Message = "Kurs bulunamadı"
                };

            }

            await _unitOfWork.BeginTransaction();

            courseEntity.UserId = course.UserId;
            courseEntity.Description = course.Description;
            courseEntity.LevelType = course.LevelType;
            courseEntity.Price = course.Price;
            courseEntity.Duration = course.Duration;
            courseEntity.StudentNumber = course.StudentNumber;


            _courseRepository.Update(courseEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();

                throw new Exception("Kurs bilgileri güncellenirken bir hata ile karşılaşıldı");
            }

            var courseLessons = _courseLessonRepository.GetAll(x => x.CourseId == x.CourseId).ToList();

            foreach (var courseLesson in courseLessons)
            {
                _courseLessonRepository.Delete(courseLesson, false);
            }

            foreach (var lessonId in course.LessonIds)
            {
                var courseLesson = new CourseLessonEntity
                {
                    CourseId = courseEntity.Id,
                    LessonId = lessonId,
                };

                _courseLessonRepository.Add(courseLesson);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Kurs bilgileri güncellenirken bir hata oluştu işlemler geri alınıyor");
            }

            return new ServiceMessage
            {
                IsSucced = true
            };


        }
    }
}
