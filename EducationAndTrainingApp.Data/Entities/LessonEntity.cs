using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EducationAndTrainingApp.Data.Entities
{
    //pyton, .net, html, css .. 
    public class LessonEntity:BaseEntity
    {
        public string Title { get; set; }


        // Relational property 

        // Bir ders birden fazla kursa ait olabilir
        public ICollection<CourseLessonEntity> CourseLessons { get; set; }

    }
    public class LessonConfiguration : BaseConfiguration<LessonEntity>
    {
        public override void Configure(EntityTypeBuilder<LessonEntity> builder)
        {
            // Lesson->CourseLesson(many - to - many)
            builder.HasMany(l => l.CourseLessons)
               .WithOne(cl => cl.Lesson)
               .HasForeignKey(cl => cl.LessonId);

            base.Configure(builder);
        }
    }
}
