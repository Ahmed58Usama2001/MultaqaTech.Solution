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

        //builder.HasOne(e => e.Instructor)
        //       .WithMany()
        //       .HasForeignKey(e => e.InstructorId)
        //       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Subject)
               .WithMany()
               .HasForeignKey(e => e.SubjectId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(bp => bp.CurriculumSections)
               .WithOne(c => c.Course)
               .HasForeignKey(c => c.CourseId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Tags)
               .WithMany(s=>s.AssociatedCourses)
               .UsingEntity(j => j.ToTable("CourseTags"));

        builder.HasMany(c => c.Prerequisites)
               .WithMany()
               .UsingEntity(j => j.ToTable("CousePrerequests"));

         builder.Property(e => e.Reviews)
                .HasConversion(v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), v => JsonSerializer.Deserialize<List<CourseReview>>(v, (JsonSerializerOptions)null));

        builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
    }
}