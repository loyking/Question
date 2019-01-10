using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using QuestionnaireSys.Model;

namespace QuestionnaireSys.DAL
{
    public class QuestionnaireSysDbContext : DbContext
    {
        public QuestionnaireSysDbContext():base("name=QuestionnaireSysDbContext")
        {

        }
        public DbSet<School> Schools { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<MatterItem> MatterItems { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<StudentAnswerDetail> StudentAnswerDetails { get; set; }
        public DbSet<Depart> Departs { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Questionnaire>().HasRequired(r => r.AdminUser)
            .WithMany(u => u.Questionnaire)
            .HasForeignKey(u => u.AdminUserId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentAnswer>().HasRequired(r => r.Student)
            .WithMany(u => u.StudentAnswer)
            .HasForeignKey(u => u.StudentId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentAnswerDetail>().HasRequired(r => r.StudentAnswer)
           .WithMany(u => u.StudentAnswerDetail)
           .HasForeignKey(u => u.StudentAnswerId)
           .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>().HasRequired(r => r.Questionnaire)
           .WithMany(u => u.Question)
           .HasForeignKey(u => u.QuestionnaireId)
           .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuestionnaireType>().HasRequired(r => r.UserRole)
           .WithMany(u => u.QuestionnaireType)
           .HasForeignKey(u => u.UserRoleId)
           .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}
