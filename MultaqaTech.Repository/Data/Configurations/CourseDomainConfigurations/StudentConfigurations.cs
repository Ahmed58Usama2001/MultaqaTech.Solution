namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations;

internal class StudentConfigurations : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasOne(s => s.AppUser) // Student or Instructor
.WithOne(u => u.Student)   // Clarifies dependent side (optional for Student)
.HasForeignKey<Student>(s => s.AppUserId) // Or Instructor.AppUserId
.OnDelete(DeleteBehavior.Restrict); // Adjust if needed

    }
}