using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Infrastructure.Configurations;

public class DepartmentSubjectConfiguration : IEntityTypeConfiguration<DepartmentSubject>
{
   public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
   {

      // configure Composite Keys
      builder.HasKey(ds => new { ds.DepartmentId, ds.SubjectId });

   }
}
