using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Infrastructure.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
   public void Configure(EntityTypeBuilder<Student> builder)
   {

      // Student-Department relationship
      builder.HasOne(s => s.Department)
             .WithMany(d => d.Students)
             .HasForeignKey(s => s.DepartmentId)
             .OnDelete(DeleteBehavior.Restrict);

   }
}
