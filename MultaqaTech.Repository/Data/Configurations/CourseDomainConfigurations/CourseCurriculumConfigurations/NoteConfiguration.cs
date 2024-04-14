namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations.CourseCurriculumConfigurations;

internal class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    const int shortMaxLength = 255;
    const int longMaxLength = 450;
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("Notes");

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(longMaxLength);

        builder.Property(e => e.LectureId)
        .IsRequired();

        builder.HasOne(bp => bp.Lecture)
                 .WithMany(c => c.Notes)
                 .HasForeignKey(bp => bp.LectureId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.NoAction);
    }
}