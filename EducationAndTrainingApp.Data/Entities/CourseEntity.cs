using EducationAndTrainingApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Data.Entities
{
    public class CourseEntity:BaseEntity
    {
        public int UserId { get; set; }       //Foreign
        public string Description { get; set; }
     
        public LevelType LevelType { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }        //Kursun süresi
        public int StudentNumber { get; set; }      //Kurstaki öğrenci sayısı

        // Relational Property
        public UserEntity User { get; set; }        //kursun eğitmeni

        // Bir kurs birden fazla dersi içerebilir
        public ICollection<CourseLessonEntity> CourseLessons { get; set; }

        // Bir kursa birden fazla öğrenci kayıt olabilir
        public ICollection<EnrollmentEntity> Enrollments { get; set; }


    }
    public class CourseConfiguration : BaseConfiguration<CourseEntity>
    {
        public override void Configure(EntityTypeBuilder<CourseEntity> builder)
        {
           

            builder.Property(x => x.Description)
                .IsRequired().
                HasMaxLength(200);

            //Course -> Enrollment (one-to-many)
            builder.HasMany(c => c.Enrollments)
               .WithOne(e => e.Course)
               .HasForeignKey(e => e.CourseId);



            //Course -> CourseLesson (many-to-many)
            builder.HasMany(c => c.CourseLessons)
               .WithOne(cl => cl.Course)
               .HasForeignKey(cl => cl.CourseId);


            base.Configure(builder);
        }
    }
}
