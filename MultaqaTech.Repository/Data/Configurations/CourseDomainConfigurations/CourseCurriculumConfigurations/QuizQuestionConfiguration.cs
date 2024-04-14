namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations.CourseCurriculumConfigurations;

internal class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
{
    const int shortMaxLength = 255;
    const int longMaxLength = 450;
    public void Configure(EntityTypeBuilder<QuizQuestion> builder)
    {
        builder.ToTable("QuizQuestions");

        builder.Property(e => e.Title)
        .IsRequired()
        .HasMaxLength(shortMaxLength);

        builder.Property(e => e.QuizId)
        .IsRequired();

        builder.HasOne(bp => bp.Quiz)
                 .WithMany(c => c.QuizQuestions)
                 .HasForeignKey(bp => bp.QuizId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(bp => bp.QuizQuestionChoices)
               .WithOne(c => c.QuizQuestion)
               .HasForeignKey(c => c.QuizQuestionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}