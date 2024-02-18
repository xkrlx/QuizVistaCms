using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuizVistaApiInfrastructureLayer.Entities;

namespace QuizVistaApiInfrastructureLayer.DbContexts;

public partial class QuizVistaDbContext : DbContext
{
    public QuizVistaDbContext()
    {
    }

    public QuizVistaDbContext(DbContextOptions<QuizVistaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Attempt> Attempts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }


    public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
    {
        return Set<TEntity>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserResults>(entity =>
        {
            entity.ToView("V_USER_RESULTS").HasNoKey();

            entity.Property(x => x.UserId).HasColumnName("USER_ID");
            entity.Property(x => x.QuizName).HasColumnName("QUIZ_NAME");
            entity.Property(x => x.QuizId).HasColumnName("QUIZ_ID");
            entity.Property(x => x.AttemptId).HasColumnName("ATTEMPT_ID");
            entity.Property(x => x.QuestionId).HasColumnName("QUESTION_ID");
            entity.Property(x => x.RegDate).HasColumnName("REGDATE");
            entity.Property(x => x.Type).HasColumnName("TYPE");
            entity.Property(x => x.AdditionalValue).HasColumnName("ADDITIONAL_VALUE");
            entity.Property(x => x.SubstractionalValue).HasColumnName("SUBSTRACTIONA_VALUE");
            entity.Property(x => x.UserCorrectAnswers).HasColumnName("USER_CORRECT_ANSWERS");
            entity.Property(x => x.UserWrongAnswers).HasColumnName("USER_WRONG_ANSWERS");
            entity.Property(x => x.MaxCorrectAnswers).HasColumnName("MAX_CORRECT_ANSWERS");
            entity.Property(x => x.MaxWrongAnswers).HasColumnName("MAX_WRONG_ANSWERS");
        });

        modelBuilder.Entity<AttemptCount>(entity =>
        {
            entity.ToView("V_ATTEMPT_COUNT").HasNoKey();

            entity.Property(x => x.UserId).HasColumnName("USER_ID");
            entity.Property(x => x.QuizId).HasColumnName("QUIZ_ID");
            entity.Property(x => x.AttemptCountNumber).HasColumnName("ATTEMPT_COUNT");

            

        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ANSWER_PK");

            entity.ToTable("ANSWER");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnswerText)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("answer_text");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");
            entity.Property(e => e.QuestionId).HasColumnName("QUESTION_id");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ANSWER_QUESTION_FK");
        });

        modelBuilder.Entity<Attempt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ATTEMPT_PK");

            entity.ToTable("ATTEMPT");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.EditionDate)
                .HasColumnType("datetime")
                .HasColumnName("edition_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Attempts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ATTEMPT_USERS_FK");

            entity.HasMany(d => d.Answers).WithMany(p => p.Attempts)
                .UsingEntity<Dictionary<string, object>>(
                    "SavedAnswer",
                    r => r.HasOne<Answer>().WithMany()
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("SAVED_ANSWERS_ANSWER_FK"),
                    l => l.HasOne<Attempt>().WithMany()
                        .HasForeignKey("AttemptId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("SAVED_ANSWERS_ATTEMPT_FK"),
                    j =>
                    {
                        j.HasKey("AttemptId", "AnswerId").HasName("SAVED_ANSWERS_PK");
                        j.ToTable("SAVED_ANSWERS");
                        j.IndexerProperty<int>("AttemptId").HasColumnName("attempt_id");
                        j.IndexerProperty<int>("AnswerId").HasColumnName("answer_id");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CATEGORY_PK");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("QUESTION_PK");

            entity.ToTable("QUESTION");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdditionalValue).HasColumnName("additional_value");
            entity.Property(e => e.CmsQuestionsStyle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cms_questions_style");
            entity.Property(e => e.CmsTitleStyle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cms_title_style");
            entity.Property(e => e.QuizId).HasColumnName("QUIZ_id");
            entity.Property(e => e.SubstractionalValue).HasColumnName("substractional_value");
            entity.Property(e => e.Text)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("text");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("1 - one good question\r\n2 - true/false\r\n3 - multi")
                .HasColumnName("type");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("QUESTION_QUIZ_FK");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("QUIZ_PK");

            entity.ToTable("QUIZ");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttemptCount).HasColumnName("attempt_count");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_id");
            entity.Property(e => e.CmsTitleStyle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cms_title_style");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.EditionDate)
                .HasColumnType("datetime")
                .HasColumnName("edition_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PublicAccess).HasColumnName("public_access");

            entity.HasOne(d => d.Author).WithMany(p => p.QuizzesNavigation)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("QUIZ_USERS_FK");

            entity.HasOne(d => d.Category).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("QUIZ_CATEGORY_FK");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ROLE_PK");

            entity.ToTable("ROLE");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TAGS_PK");

            entity.ToTable("TAGS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasMany(d => d.Quizzes).WithMany(p => p.Tags)
                .UsingEntity<Dictionary<string, object>>(
                    "TagsQuiz",
                    r => r.HasOne<Quiz>().WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("TAGS_QUIZ_QUIZ_FK"),
                    l => l.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("TAGS_QUIZ_TAGS_FK"),
                    j =>
                    {
                        j.HasKey("TagId", "QuizId").HasName("TAGS_QUIZ_PK");
                        j.ToTable("TAGS_QUIZ");
                        j.IndexerProperty<int>("TagId").HasColumnName("tag_id");
                        j.IndexerProperty<int>("QuizId").HasColumnName("quiz_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("USERS_PK");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Email, "USERS_email_UN").IsUnique();

            entity.HasIndex(e => e.UserName, "USERS_user_name_UN").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordResetExpire)
                .HasColumnType("datetime2")
                .HasColumnName("reset_password_expire");
            entity.Property(e => e.ResetPasswordToken)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("reset_password_token");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("user_name");

            entity.HasMany(d => d.Quizzes).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AccessQuiz",
                    r => r.HasOne<Quiz>().WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("ACCESS_QUIZ_QUIZ_FK"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("ACCESS_QUIZ_USERS_FK"),
                    j =>
                    {
                        j.HasKey("UserId", "QuizId").HasName("ACCESS_QUIZ_PK");
                        j.ToTable("ACCESS_QUIZ");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("QuizId").HasColumnName("quiz_id");
                    });

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("USER_ROLE_ROLE_FK"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("USER_ROLE_USERS_FK"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("USER_ROLE_PK");
                        j.ToTable("USER_ROLE");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
