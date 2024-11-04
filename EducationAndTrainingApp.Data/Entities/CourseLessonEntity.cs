using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Data.Entities
{
    //Bu ara tablo CourseLessonEntity => ÇOKA ÇOK bağlantı
    public class CourseLessonEntity:BaseEntity
    {
        public int CourseId { get; set; }
        public int LessonId { get; set; }


        //Relational property

        //Bir kurs özelliği bir kursa ait olacak
        public CourseEntity Course { get; set; }
        public LessonEntity Lesson { get; set; }
    }

    public class CourseLessonConfiguration : BaseConfiguration<CourseLessonEntity>
    {
        public override void Configure(EntityTypeBuilder<CourseLessonEntity> builder)
        {
            builder.Ignore(x => x.Id);  

            builder.HasKey("CourseId", "LessonId");     //Composite key oluşturulup yeni primary key olarak atandı.

            builder.HasOne(cl => cl.Course)
               .WithMany(c => c.CourseLessons)
               .HasForeignKey(cl => cl.CourseId);

            builder.HasOne(cl => cl.Lesson)
                   .WithMany(l => l.CourseLessons)
                   .HasForeignKey(cl => cl.LessonId);





            base.Configure(builder);
        }
    }
}
