using SchoolProject.Domain.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Domain.Entities;

public class Instructor : GeneralLocalizableEntity
{
   public Instructor()
   {
      Instructors = new HashSet<Instructor>();
      Ins_Subjects = new HashSet<InstructorSubject>();
   }

   [Key]
   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   public int Id { get; set; }

   public string? Name_Ar { get; set; }
   public string? Name_En { get; set; }

   public string? Address { get; set; }
   public string? Position { get; set; }

   public decimal? Salary { get; set; }
   public string? Image { get; set; }

   public int? SupervisorId { get; set; }

   public int DepartmentId { get; set; }

   //Relationship 1: The Department this Instructor works for
   [ForeignKey(nameof(DepartmentId))]
   [InverseProperty(nameof(Entities.Department.Instructors))]
   public Department? Department { get; set; }

   // Relationship 2: The Department this Instructor Manages
   [InverseProperty(nameof(Entities.Department.Manager))]
   public Department? DepartmentManager { get; set; }

   // Self-Referencing Relationship
   [ForeignKey(nameof(SupervisorId))]
   [InverseProperty(nameof(Instructors))]
   public Instructor? Supervisor { get; set; }

   [InverseProperty(nameof(Supervisor))]
   public virtual ICollection<Instructor> Instructors { get; set; }

   [InverseProperty(nameof(InstructorSubject.Instructor))]
   public virtual ICollection<InstructorSubject> Ins_Subjects { get; set; }

}
