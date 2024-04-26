namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations.CourseCurriculumConfigurations;

internal class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    const int shortMaxLength = 255;
    const int longMaxLength = 450;
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.ToTable("Quizes");


        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(shortMaxLength);
        
        builder.Property(e => e.Description)
            .HasMaxLength(longMaxLength);

        builder.Property(e => e.CurriculumSectionId)
        .IsRequired();

        builder.Ignore(l => l.MediaUrl);
        builder.Ignore(l => l.CurriculumItemType);


        builder.HasOne(bp => bp.CurriculumSection)
                 .WithMany(c => c.Quizes)
                 .HasForeignKey(bp => bp.CurriculumSectionId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(bp => bp.QuizQuestions)
        .WithOne(c => c.Quiz)
        .HasForeignKey(c => c.QuizId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}