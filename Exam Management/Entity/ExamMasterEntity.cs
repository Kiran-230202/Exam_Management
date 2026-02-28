using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam_Management.Entity
{
    public class ExamMasterEntity
    {
        [Key]
        public int MasterID { get; set; }

        [Required(ErrorMessage = "Student must be selected.")]
        [Range(1, int.MaxValue, ErrorMessage = "Student must be selected.")]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "Exam year is required.")]
        [Range(2000, 2100, ErrorMessage = "Exam year must be valid.")]
        public int ExamYear { get; set; }

        [Required]
        public int TotalMark { get; set; }

        [Required]
        [StringLength(10)]
        public string ?PassOrFail { get; set; } = string.Empty;

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.Now;
        // Navigation
        [ForeignKey("StudentID")]
        public StudentEntity? Student { get; set; }

        public ICollection<ExamDtlsEntity> ?ExamDtls { get; set; }
    }
}
