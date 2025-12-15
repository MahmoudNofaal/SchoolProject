using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Infrastructure.Configurations;

public class InstructorSubjectConfiguration : IEntityTypeConfiguration<InstructorSubject>
{
   public void Configure(EntityTypeBuilder<InstructorSubject> builder)
   {

      // configure Composite Keys
      builder.HasKey(ins => new { ins.InstructorId, ins.SubjectId });

   }
}
