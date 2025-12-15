using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Domain.Entities;

public class StudentSubject
{
   [Key]
   public int StudentId { get; set; }
   [Key]
   public int SubjectId { get; set; }

   public decimal? Grade { get; set; }

   [ForeignKey(nameof(StudentId))]
   [InverseProperty(nameof(Entities.Student.StudentSubject))]
   public virtual Student? Student { get; set; }

   [ForeignKey(nameof(SubjectId))]
   [InverseProperty(nameof(Entities.Subject.StudentsSubjects))]
   public virtual Subject? Subject { get; set; }

}
