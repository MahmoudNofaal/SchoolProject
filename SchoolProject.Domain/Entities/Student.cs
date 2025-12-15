using SchoolProject.Domain.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Domain.Entities;

public class Student : GeneralLocalizableEntity
{
   public Student()
   {
      StudentSubject = new HashSet<StudentSubject>();
   }

   [Key]
   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   public int Id { get; set; }

   public string? Name_Ar { get; set; }
   public string? Name_En { get; set; }

   [StringLength(500)]
   public string? Address { get; set; }


   [StringLength(20)]
   public string? Phone { get; set; }

   public int? DepartmentId { get; set; }

   [ForeignKey(nameof(DepartmentId))]
   [InverseProperty(nameof(Entities.Department.Students))]
   public virtual Department? Department { get; set; }

   [InverseProperty(nameof(Entities.StudentSubject.Student))]
   public virtual ICollection<StudentSubject> StudentSubject { get; set; }

}
