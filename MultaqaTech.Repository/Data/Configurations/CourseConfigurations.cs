namespace MultaqaTech.Repository.Data.Configurations;

internal class CourseConfigurations : IEntityTypeConfiguration<Course>
{
    const int maxLength = 100;

    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.Property(e => e.Title)
               .IsRequired()
               .HasMaxLength(maxLength);

        builder.HasIndex(e => e.Title).IsUnique();

        builder.HasOne(e => e.Subject)
               .WithMany()
               .HasForeignKey(e => e.SubjectId)
               .OnDelete(DeleteBehavior.Restrict);
        builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
    }
}
