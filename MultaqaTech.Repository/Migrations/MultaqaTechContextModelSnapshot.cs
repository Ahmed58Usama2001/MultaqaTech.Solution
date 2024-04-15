﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MultaqaTech.Repository.Data.Configurations;

#nullable disable

namespace MultaqaTech.Repository.Migrations
{
    [DbContext(typeof(MultaqaTechContext))]
    partial class MultaqaTechContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlogPostSubject", b =>
                {
                    b.Property<int>("BlogPostsId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("BlogPostsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("BlogPostSubject");
                });

            modelBuilder.Entity("CourseSubject", b =>
                {
                    b.Property<int>("AssociatedCoursesId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("AssociatedCoursesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("CourseTags", (string)null);
                });

            modelBuilder.Entity("CourseSubject1", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("PrerequisitesId")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "PrerequisitesId");

                    b.HasIndex("PrerequisitesId");

                    b.ToTable("CousePrerequests", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.BlogPostDomainEntities.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BlogPostCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MediaUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfViews")
                        .HasColumnType("int");

                    b.Property<string>("PostPictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("BlogPostCategoryId");

                    b.ToTable("BlogPosts", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.BlogPostDomainEntities.BlogPostCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("BlogPostCategories", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.BlogPostDomainEntities.BlogPostComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BlogPostId")
                        .HasColumnType("int");

                    b.Property<string>("CommentContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId");

                    b.ToTable("BlogPostComments", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DeductionAmount")
                        .HasColumnType("int");

                    b.Property<int>("DeductionType")
                        .HasColumnType("int");

                    b.Property<decimal>("Duration")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("EnrolledStudentsIds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstructorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LearningObjectives")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LecturesLinks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Reviews")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TotalEnrolled")
                        .HasColumnType("int");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AnswererId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.CurriculumSection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CurriculumSections", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Lecture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurriculumSectionId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurriculumSectionId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Lectures", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("LectureId")
                        .HasColumnType("int");

                    b.Property<string>("WriterStudentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.ToTable("Notes", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AskerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("LectureId")
                        .HasColumnType("int");

                    b.Property<string>("QuestionPictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.ToTable("Questions", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurriculumSectionId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("QuizQuestionPictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CurriculumSectionId");

                    b.ToTable("Quizes", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.QuizQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("QuizId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("QuizQuestions", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.QuizQuestionChoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsRight")
                        .HasColumnType("bit");

                    b.Property<int>("QuizQuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("QuizQuestionId");

                    b.ToTable("QuizQuestionChoices", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.SettingsEntities.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Subjects", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.ZoomDomainEntites.ZoomMeeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("MeetingId")
                        .HasColumnType("int");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("ZoomMeetingCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("ZoomMeetingCategoryId");

                    b.ToTable("ZoomMeetings", (string)null);
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.ZoomDomainEntites.ZoomMeetingCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("ZoomMeetingCategories", (string)null);
                });

            modelBuilder.Entity("BlogPostSubject", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.BlogPostDomainEntities.BlogPost", null)
                        .WithMany()
                        .HasForeignKey("BlogPostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultaqaTech.Core.Entities.SettingsEntities.Subject", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseSubject", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.CourseDomainEntities.Course", null)
                        .WithMany()
                        .HasForeignKey("AssociatedCoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultaqaTech.Core.Entities.SettingsEntities.Subject", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseSubject1", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.CourseDomainEntities.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultaqaTech.Core.Entities.SettingsEntities.Subject", null)
                        .WithMany()
                        .HasForeignKey("PrerequisitesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.BlogPostDomainEntities.BlogPost", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.BlogPostDomainEntities.BlogPostCategory", "Category")
                        .WithMany("BlogPosts")
                        .HasForeignKey("BlogPostCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.BlogPostDomainEntities.BlogPostComment", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.BlogPostDomainEntities.BlogPost", "BlogPost")
                        .WithMany("Comments")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BlogPost");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.Course", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.SettingsEntities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Answer", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.CurriculumSection", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.CourseDomainEntities.Course", "Course")
                        .WithMany("CurriculumSections")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Lecture", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.CurriculumSection", "CurriculumSection")
                        .WithMany("Lectures")
                        .HasForeignKey("CurriculumSectionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CurriculumSection");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Note", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Lecture", "Lecture")
                        .WithMany("Notes")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Question", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Lecture", "Lecture")
                        .WithMany("Questions")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Lecture");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Quiz", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.CurriculumSection", "CurriculumSection")
                        .WithMany("Quizes")
                        .HasForeignKey("CurriculumSectionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CurriculumSection");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.QuizQuestion", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Quiz", "Quiz")
                        .WithMany("QuizQuestions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.QuizQuestionChoice", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.QuizQuestion", "QuizQuestion")
                        .WithMany("QuizQuestionChoices")
                        .HasForeignKey("QuizQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuizQuestion");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.ZoomDomainEntites.ZoomMeeting", b =>
                {
                    b.HasOne("MultaqaTech.Core.Entities.SettingsEntities.Subject", null)
                        .WithMany("ZoomMeetings")
                        .HasForeignKey("SubjectId");

                    b.HasOne("MultaqaTech.Core.Entities.ZoomDomainEntites.ZoomMeetingCategory", "Category")
                        .WithMany("ZoomMeetings")
                        .HasForeignKey("ZoomMeetingCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.BlogPostDomainEntities.BlogPost", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.BlogPostDomainEntities.BlogPostCategory", b =>
                {
                    b.Navigation("BlogPosts");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.Course", b =>
                {
                    b.Navigation("CurriculumSections");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.CurriculumSection", b =>
                {
                    b.Navigation("Lectures");

                    b.Navigation("Quizes");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Lecture", b =>
                {
                    b.Navigation("Notes");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.Quiz", b =>
                {
                    b.Navigation("QuizQuestions");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.CourseDomainEntities.CurriculumDomainEntities.QuizQuestion", b =>
                {
                    b.Navigation("QuizQuestionChoices");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.SettingsEntities.Subject", b =>
                {
                    b.Navigation("ZoomMeetings");
                });

            modelBuilder.Entity("MultaqaTech.Core.Entities.ZoomDomainEntites.ZoomMeetingCategory", b =>
                {
                    b.Navigation("ZoomMeetings");
                });
#pragma warning restore 612, 618
        }
    }
}
