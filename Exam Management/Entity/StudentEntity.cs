using System.ComponentModel.DataAnnotations;

namespace Exam_Management.Entity
{
    public class StudentEntity
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(250)]
        public string StudentName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Mail { get; set; }

        // Navigation Property
        public ICollection<ExamMasterEntity> ExamMasters { get; set; }
    }
}
