namespace MultaqaTech.Repository.Data.Configurations;

public class MultaqaTechContext : DbContext
{

    public MultaqaTechContext(DbContextOptions<MultaqaTechContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    public DbSet<Subject> Subjects { get; set; }

    #region Blog Posts
    public DbSet<BlogPostCategory> BlogPostCategories { get; set; }
    public DbSet<BlogPostComment> BlogPostComments { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
    #endregion

    #region Courses
    public DbSet<Course> Courses { get; set; }

    public DbSet<CurriculumSection> CurriculumSections { get; set; }


    public DbSet<Lecture> Lectures { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }

    public DbSet<Quiz> Quizes { get; set; }
    public DbSet<QuizQuestion> QuizQuestions { get; set; }
    public DbSet<QuizQuestionChoice> QuizQuestionChoices { get; set; } 
    #endregion

    #region Zoom Meetings
    public DbSet<ZoomMeetingCategory> ZoomMeetingCategories { get; set; }
    public DbSet<ZoomMeeting> ZoomMeetings { get; set; } 
    #endregion
}