namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations.CourseCurriculumConfigurations;

internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    const int shortMaxLength = 255;
    const int longMaxLength = 450;
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.Property(e => e.Title)
        .IsRequired()
        .HasMaxLength(shortMaxLength);

        builder.Property(e => e.QuizId)
        .IsRequired();

        builder.HasOne(bp => bp.Quiz)
                 .WithMany(c => c.Questions)
                 .HasForeignKey(bp => bp.QuizId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(bp => bp.Answers)
               .WithOne(c => c.Question)
               .HasForeignKey(c => c.QuestionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}