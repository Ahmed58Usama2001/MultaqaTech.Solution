namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations.CourseCurriculumConfigurations;

internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    const int shortMaxLength = 255;
    const int longMaxLength = 450;
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasIndex(e => e.LectureId);

        builder.Property(e => e.Content)
            .HasMaxLength(longMaxLength);

        builder.Property(e => e.LectureId)
        .IsRequired();

        builder.Property(e => e.PublishingDate)
       .IsRequired();

        builder.Ignore(l => l.MediaUrl);

        builder.HasOne(bp => bp.Lecture)
                 .WithMany(c => c.Questions)
                 .HasForeignKey(bp => bp.LectureId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(bp => bp.Answers)
       .WithOne(c => c.Question)
       .HasForeignKey(c => c.QuestionId)
       .OnDelete(DeleteBehavior.Cascade);

  
    }
}