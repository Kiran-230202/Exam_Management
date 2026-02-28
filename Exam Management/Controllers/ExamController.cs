using Exam_Management.Data;
using Exam_Management.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExamController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("SaveExam")]
            public async Task<IActionResult> SaveExam([FromBody] ExamMasterEntity examMaster)
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new { errors });
                }
                if (examMaster.ExamDtls == null || !examMaster.ExamDtls.Any())
                    return BadRequest(new { message = "At least one subject is required." });

                bool exists = await _context.ExamMasters
                    .AnyAsync(e => e.StudentID == examMaster.StudentID && e.ExamYear == examMaster.ExamYear);

                if (exists)
                    return BadRequest(new { message = "Exam for this student and year already exists." });

                examMaster.TotalMark = examMaster.ExamDtls.Sum(x => x.Marks);

                if (examMaster.PassOrFail != (examMaster.ExamDtls.All(x => x.Marks >= 25) ? "PASS" : "FAIL"))
                {
                    examMaster.PassOrFail = examMaster.ExamDtls.All(x => x.Marks >= 25) ? "PASS" : "FAIL";
                }

                if (examMaster.CreateTime == default)
                    examMaster.CreateTime = DateTime.Now;

                _context.ExamMasters.Add(examMaster);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("UX_Student_Email"))
                        return BadRequest("Email must be unique.");
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("UX_ExamMaster_Student_Year"))
                        return BadRequest("Exam for this student and year already exists.");

                    throw;
                }

                return Ok(new
                {
                    Message = "Exam saved successfully",
                    TotalMark = examMaster.TotalMark,
                    Result = examMaster.PassOrFail
                });
            }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _context.Students
                .Select(s => new { s.StudentID, s.StudentName, s.Mail })
                .ToListAsync();
            return Ok(students);
        }

        [HttpGet("GetAllSubjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _context.SubjectMsts
                .Select(s => new { s.SubjectID, s.SubjectName })
                .ToListAsync();
            return Ok(subjects);
        }

        [HttpGet("GetSavedExams")]
        public async Task<IActionResult> GetSavedExams()
        {
            var results = await _context.ExamMasters
                .Include(e => e.Student) // include student info
                .Include(e => e.ExamDtls) // include exam details
                    .ThenInclude(d => d.SubjectEntity) // include subject info
                .Select(e => new
                {
                    e.MasterID,
                    StudentName = e.Student.StudentName,
                    e.ExamYear,
                    e.TotalMark,
                    e.PassOrFail,
                    Subjects = e.ExamDtls.Select(d => new
                    {
                        SubjectName = d.SubjectEntity.SubjectName,
                        d.Marks
                    }).ToList()
                })
                .ToListAsync();

            return Ok(results);
        }

    }
}
