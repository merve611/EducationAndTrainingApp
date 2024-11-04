﻿using EducationAndTrainingApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationAndTrainingApp.Data.Entities
{
    public class UserEntity :BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public UserType UserType { get; set; }


        // Relational Property

        //Bir kullanıcı birden fazla kursa kayıt yaptırabilir
        public ICollection<EnrollmentEntity> Enrollments { get; set; }
       
        
    }

    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            //User -> Enrollment (one-to-many)

            builder.HasMany(u => u.Enrollments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);



            base.Configure(builder);
        }
    }
   


}
