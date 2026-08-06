using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinguaFlow.Models;

namespace Linguaflow.DAL
{
    public class LinguaFlowContext : DbContext
    {
        public LinguaFlowContext(DbContextOptions<LinguaFlowContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<TutorFee> TutorFees { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PRIMARY KEYS
            modelBuilder.Entity<Language>().HasKey(l => l.Id);
            modelBuilder.Entity<Tutor>().HasKey(t => t.Id);
            modelBuilder.Entity<Course>().HasKey(c => c.Id);
            modelBuilder.Entity<Student>().HasKey(s => s.Id);
            modelBuilder.Entity<Enrollment>().HasKey(e => e.Id);
            modelBuilder.Entity<TutorFee>().HasKey(tf => tf.Id);
            modelBuilder.Entity<Payment>().HasKey(p => p.Id);


            // PROPERTIES AND CONSTRAINTS

            // Language
            modelBuilder.Entity<Language>()
                .Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(50);


            // Tutor
            modelBuilder.Entity<Tutor>()
                .Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Tutor>()
                .Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Tutor>()
                .Property(t => t.Bio)
                .HasMaxLength(500);

            modelBuilder.Entity<Tutor>()
                .Property(t => t.Availability)
                .IsRequired()
                .HasMaxLength(200);


            // Course
            modelBuilder.Entity<Course>()
                .Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Course>()
                .Property(c => c.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .IsRequired();


            // Student
            modelBuilder.Entity<Student>()
                .Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Student>()
                .Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Student>()
                .Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(100);


            // Enrollment
            modelBuilder.Entity<Enrollment>()
                .Property(e => e.EnrollmentDate)
                .IsRequired();

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20); 


            // Tutor Fee
            modelBuilder.Entity<TutorFee>()
                .Property(tf => tf.FeeAmount)
                .IsRequired();

            modelBuilder.Entity<TutorFee>()
                .Property(tf => tf.Status)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<TutorFee>()
                .Property(tf => tf.CreatedAt)
                .IsRequired();

            modelBuilder.Entity<TutorFee>()
                .Property(tf => tf.PaidAt);
              

            // Payment
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .IsRequired();

            modelBuilder.Entity<Payment>()
                .Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Method)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Payment>()
                .Property(p => p.CreatedAt)
                .IsRequired();

            modelBuilder.Entity<Payment>()
                .Property(p => p.PaidAt);
               

           //RELATIONSHIPS

            // LANGUAGE (1:N) TUTORS
            modelBuilder.Entity<Tutor>()
                .HasOne(t => t.Language)
                .WithMany(l => l.Tutors)
                .HasForeignKey(t => t.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            // TUTOR - COURSE (N:N)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Tutors)
                .WithMany(t => t.Courses);

            // STUDENT - ENROLLMENT (1:N)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // COURSE - ENROLLMENT (1:N)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // TUTOR - TUTOR FEE (1:N)
            modelBuilder.Entity<TutorFee>()
                .HasOne(tf => tf.Tutor)
                .WithMany(t => t.TutorFees)
                .HasForeignKey(tf => tf.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

            // ENROLLMENT - TUTOR FEE (1:1)
            modelBuilder.Entity<TutorFee>()
                 .HasOne(tf => tf.Enrollment)
                 .WithOne(e => e.TutorFee)
                 .HasForeignKey<TutorFee>(tf => tf.EnrollmentId)
                 .OnDelete(DeleteBehavior.Restrict);


            // ENROLLMENT - PAYMENT (1:1)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Enrollment)
                .WithOne(e => e.Payment)
                .HasForeignKey<Payment>(p => p.EnrollmentId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

