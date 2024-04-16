namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations.CourseCurriculumConfigurations;

internal class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    const int shortMaxLength = 255;
    const int longMaxLength = 450;
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.ToTable("Answers");

        builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(longMaxLength);

        builder.Property(e => e.QuestionId)
        .IsRequired();

        builder.Property(e => e.PublishingDate)
       .IsRequired();

        builder.HasOne(bp => bp.Question)
                 .WithMany(c => c.Answers)
                 .HasForeignKey(bp => bp.QuestionId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);


    }
}