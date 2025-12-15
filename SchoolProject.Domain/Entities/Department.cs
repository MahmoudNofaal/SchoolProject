using SchoolProject.Domain.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Domain.Entities;

public partial class Department : GeneralLocalizableEntity
{
   public Department()
   {
      Students = new HashSet<Student>();
      DepartmentSubjects = new HashSet<DepartmentSubject>();
      Instructors = new HashSet<Instructor>();
   }

   [Key]
   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   public int Id { get; set; }

   [StringLength(200)]
   public string? Name_Ar { get; set; }
   [StringLength(200)]
   public string? Name_En { get; set; }

   // Foreign Key for the Manager
   public int? ManagerId { get; set; }

   [InverseProperty(nameof(Entities.Student.Department))]
   public virtual ICollection<Student> Students { get; set; }

   // Pointed to DepartmentSubject
   [InverseProperty(nameof(Entities.DepartmentSubject.Department))]
   public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }

   // Pointed to Instructor.Department (The employment relationship)
   [InverseProperty(nameof(Entities.Instructor.Department))]
   public virtual ICollection<Instructor> Instructors { get; set; }

   [ForeignKey(nameof(ManagerId))]
   [InverseProperty(nameof(Entities.Instructor.DepartmentManager))]
   public virtual Instructor? Manager { get; set; }

}
