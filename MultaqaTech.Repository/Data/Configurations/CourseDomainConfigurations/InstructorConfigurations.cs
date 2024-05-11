namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations;

internal class InstructorConfigurations : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructors");

        builder.HasMany(i => i.Courses)
               .WithOne(c => c.Instructor)
               .HasForeignKey(c => c.InstructorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.AppUser) // Student or Instructor
    .WithOne(u => u.Instructor)   // Clarifies dependent side (optional for Student)
    .HasForeignKey<Instructor>(s => s.AppUserId) // Or Instructor.AppUserId
    .OnDelete(DeleteBehavior.Restrict); // Adjust if needed

    }
}