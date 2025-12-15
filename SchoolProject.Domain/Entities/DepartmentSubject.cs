using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Domain.Entities;

public class DepartmentSubject
{
   [Key]
   public int DepartmentId { get; set; }

   [Key]
   public int SubjectId { get; set; }

   [ForeignKey(nameof(DepartmentId))]
   [InverseProperty(nameof(Entities.Department.DepartmentSubjects))]
   public virtual Department? Department { get; set; }

   [ForeignKey(nameof(SubjectId))]
   [InverseProperty(nameof(Entities.Subject.DepartmetsSubjects))]
   public virtual Subject? Subject { get; set; }

}
