using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exam_Management.Entity
{
    public class ExamDtlsEntity
    {
        [Key]
        public int DtlsID { get; set; }

        [Required]
        public int MasterID { get; set; }

        [Required]
        public int SubjectID { get; set; }

        [Required(ErrorMessage = "Marks are required.")]
        [Range(0, 100, ErrorMessage = "Marks must be between 0 and 100.")]
        public int Marks { get; set; }

        // Navigation
        [ForeignKey("MasterID")]
        public ExamMasterEntity? ExamMaster { get; set; }
        [ForeignKey("SubjectID")]
        public SubjectEntity? SubjectEntity { get; set; }
    }
}
