namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations.CourseCurriculumConfigurations;

internal class LectureConfiguration : IEntityTypeConfiguration<Lecture>
{
    const int shortMaxLength = 255;
    const int longMaxLength = 450;
    public void Configure(EntityTypeBuilder<Lecture> builder)
    {
        builder.ToTable("Lectures");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(shortMaxLength);

        builder.Property(e => e.VideoUrl)
            .IsRequired();
        
        builder.Property(e => e.Description)
            .HasMaxLength(longMaxLength);

        builder.Property(e => e.CurriculumSectionId)
        .IsRequired();

        builder.Ignore(l => l.MediaUrl);

        builder.HasOne(bp => bp.CurriculumSection)
                 .WithMany(c => c.Lectures)
                 .HasForeignKey(bp => bp.CurriculumSectionId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(bp => bp.Notes)
        .WithOne(c => c.Lecture)
        .HasForeignKey(c => c.LectureId)
        .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(bp => bp.Questions)
        .WithOne(c => c.Lecture)
        .HasForeignKey(c => c.LectureId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}