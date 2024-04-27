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

        builder.Ignore(l => l.MediaUrl);

        builder.HasOne(e => e.Instractor)
               .WithMany(i => i.Courses)
               .HasForeignKey(e => e.InstractorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Subject)
               .WithMany()
               .HasForeignKey(e => e.SubjectId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(bp => bp.CurriculumSections)
               .WithOne(c => c.Course)
               .HasForeignKey(c => c.CourseId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Tags)
         .WithMany(s => s.AssociatedCoursesTags)
         .UsingEntity(j => j.ToTable("CourseTags"));

        builder.HasMany(c => c.Prerequisites)
          .WithMany()
          .UsingEntity(j => j.ToTable("CoursePrerequisites"));

        builder.Property(e => e.Reviews)
               .HasConversion(v => JsonSerializer
               .Serialize(v, (JsonSerializerOptions?)null), v => JsonSerializer
               .Deserialize<List<CourseReview>>(v, (JsonSerializerOptions?)null));

        builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
    }
}