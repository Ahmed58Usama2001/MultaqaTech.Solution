namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations.CourseCurriculumConfigurations;

internal class QuizQuestionChoiceConfiguration : IEntityTypeConfiguration<QuizQuestionChoice>
{
    const int shortMaxLength = 255;
    const int longMaxLength = 450;
    public void Configure(EntityTypeBuilder<QuizQuestionChoice> builder)
    {
        builder.ToTable("QuizQuestionChoices");

        builder.HasIndex(e => e.QuizQuestionId);

        builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(shortMaxLength);
        
        builder.Property(e => e.Clarification)
            .HasMaxLength(longMaxLength);

        builder.Property(e => e.QuizQuestionId)
        .IsRequired();


        builder.HasOne(bp => bp.QuizQuestion)
                 .WithMany(c => c.QuizQuestionChoices)
                 .HasForeignKey(bp => bp.QuizQuestionId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);
    }
}