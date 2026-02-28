using Exam_Management.Entity;
using Microsoft.EntityFrameworkCore;
namespace Exam_Management.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<ExamMasterEntity> ExamMasters { get; set; }
        public DbSet<ExamDtlsEntity> ExamDtls { get; set; }
        public DbSet<SubjectEntity> SubjectMsts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentEntity>()
                .HasIndex(s => s.Mail)
                .IsUnique();

            modelBuilder.Entity<StudentEntity>()
                .HasMany(s => s.ExamMasters)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamMasterEntity>()
                .HasMany(m => m.ExamDtls)
                .WithOne(d => d.ExamMaster)
                .HasForeignKey(d => d.MasterID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubjectEntity>()
                .HasMany(s => s.ExamDtls)
                .WithOne(d => d.SubjectEntity)
                .HasForeignKey(d => d.SubjectID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
