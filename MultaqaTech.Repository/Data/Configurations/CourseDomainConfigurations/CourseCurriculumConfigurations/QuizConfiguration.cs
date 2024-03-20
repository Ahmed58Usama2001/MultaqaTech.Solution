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


        builder.HasOne(bp => bp.CurriculumSection)
                 .WithMany(c => c.Quizes)
                 .HasForeignKey(bp => bp.CurriculumSectionId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(bp => bp.Questions)
        .WithOne(c => c.Quiz)
        .HasForeignKey(c => c.QuizId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}