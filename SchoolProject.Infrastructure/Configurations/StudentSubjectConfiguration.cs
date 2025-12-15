using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Infrastructure.Configurations;

public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
{
   public void Configure(EntityTypeBuilder<StudentSubject> builder)
   {

      // configure Composite Keys
      builder.HasKey(ss => new { ss.StudentId, ss.SubjectId });

   }
}
