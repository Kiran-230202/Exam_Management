using System.ComponentModel.DataAnnotations;

namespace Exam_Management.Entity
{
    public class SubjectEntity
    {
        [Key]
        public int SubjectID { get; set; }

        [Required]
        [StringLength(150)]
        public string SubjectName { get; set; }
        public ICollection<ExamDtlsEntity> ExamDtls { get; set; }
    }
}
