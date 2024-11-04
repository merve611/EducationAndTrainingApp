using EducationAndTrainingApp.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Data.Entities
{
    // Hangi kursa hangi kullanıcı kayıt yaptırdı
    public class EnrollmentEntity :BaseEntity       //Kurs Kaydı
    {
        public int CourseId { get; set; }           //Foreign key
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }     //kursun başlangıç tarihi
        public DateTime EndDate { get; set; }
        


        // Relational Property
        public CourseEntity Course { get; set; }
        public UserEntity User { get; set; }

    }

    public class EnrollmentConfiguration : BaseConfiguration<EnrollmentEntity>
    {
        public override void Configure(EntityTypeBuilder<EnrollmentEntity> builder)
        {
            builder.HasOne(e => e.User)
               .WithMany(u => u.Enrollments)
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(e => e.Course)
                   .WithMany(c => c.Enrollments)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.NoAction);


            base.Configure(builder);
        }
    }
}
