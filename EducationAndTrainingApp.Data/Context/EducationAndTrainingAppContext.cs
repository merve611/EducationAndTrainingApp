using EducationAndTrainingApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Data.Context
{
    public class EducationAndTrainingAppContext :DbContext
    {
        public EducationAndTrainingAppContext(DbContextOptions<EducationAndTrainingAppContext> options) : base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent Api

            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new CourseLessonConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity
                {
                    Id = 1,
                    MaintenenceMode = false
                });

            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<LessonEntity> Lessons => Set<LessonEntity>();
        public DbSet<CourseEntity> Courses => Set<CourseEntity>();
        public DbSet<CourseLessonEntity> CourseLessons => Set<CourseLessonEntity>();
        public DbSet<EnrollmentEntity> Enrollments => Set<EnrollmentEntity>();
        public DbSet<SettingEntity> Settings => Set<SettingEntity>();



    }
}
