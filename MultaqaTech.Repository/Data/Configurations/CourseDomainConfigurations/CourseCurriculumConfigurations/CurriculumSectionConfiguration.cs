namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations.CourseCurriculumConfigurations;

internal class CurriculumSectionConfiguration : IEntityTypeConfiguration<CurriculumSection>
{
    const int maxLength = 255;
    public void Configure(EntityTypeBuilder<CurriculumSection> builder)
    {
        builder.ToTable("CurriculumSections");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(maxLength);

        builder.Property(e => e.CourseId)
        .IsRequired();

        builder.HasOne(bp => bp.Course)
                 .WithMany(c => c.CurriculumSections)
                 .HasForeignKey(bp => bp.CourseId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(bp => bp.Lectures)
               .WithOne(c => c.CurriculumSection)
               .HasForeignKey(c => c.CurriculumSectionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(bp => bp.Quizes)
         .WithOne(c => c.CurriculumSection)
         .HasForeignKey(c => c.CurriculumSectionId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}