namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations;

internal class CourseConfigurations : IEntityTypeConfiguration<Course>
{
    const int maxLength = 100;

    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasIndex(e => e.Title).IsUnique();

        builder.Property(e => e.Title)
               .IsRequired()
               .HasMaxLength(maxLength);

        builder.HasOne(e => e.Subject)
               .WithMany()
               .HasForeignKey(e => e.SubjectId)
               .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(e => e.Instructor)
        //       .WithMany()
        //       .HasForeignKey(e => e.InstructorId)
        //       .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Tags)
               .WithOne(ct => ct.Course)
               .HasForeignKey(ct => ct.CourseId);

        builder.HasMany(e => e.Prerequisites)
               .WithOne(cp => cp.Course)
               .HasForeignKey(cp => cp.CourseId);

        builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
    }
}