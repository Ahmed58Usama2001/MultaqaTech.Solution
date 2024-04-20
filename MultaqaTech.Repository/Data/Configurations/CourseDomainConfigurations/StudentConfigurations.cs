namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations;

internal class StudentConfigurations : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasMany(bp => bp.Courses)
               .WithMany(c => c.Students);
    }
}