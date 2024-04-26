namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations.CourseCurriculumConfigurations;

internal class CurriculumSectionConfiguration : IEntityTypeConfiguration<CurriculumSection>
{
    const int shortMaxLength = 255;
    const int longMaxLength = 450;
    public void Configure(EntityTypeBuilder<CurriculumSection> builder)
    {
        builder.ToTable("CurriculumSections");


        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(shortMaxLength);


        builder.Property(e => e.Objectives)
            .HasMaxLength(longMaxLength);

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