using SchoolProject.Domain.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Domain.Entities;

public class Subject : GeneralLocalizableEntity
{
   public Subject()
   {
      StudentsSubjects = new HashSet<StudentSubject>();
      DepartmetsSubjects = new HashSet<DepartmentSubject>();
      InstrcutorSubjects = new HashSet<InstructorSubject>();
   }

   [Key]
   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   public int Id { get; set; }

   [StringLength(500)]
   public string? Name_Ar { get; set; }
   [StringLength(500)]
   public string? Name_En { get; set; }

   public int? PeriodInHours { get; set; } // Duration of the subject in hours

   [InverseProperty(nameof(Entities.StudentSubject.Subject))]
   public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }

   [InverseProperty(nameof(DepartmentSubject.Subject))]
   public virtual ICollection<DepartmentSubject> DepartmetsSubjects { get; set; }

   [InverseProperty(nameof(InstructorSubject.Subject))]
   public virtual ICollection<InstructorSubject> InstrcutorSubjects { get; set; }

}
