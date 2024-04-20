namespace MultaqaTech.Repository.Data.Configurations.CourseDomainConfigurations;

internal class InstructorConfigurations : IEntityTypeConfiguration<Instractor>
{
    public void Configure(EntityTypeBuilder<Instractor> builder)
    {
        builder.ToTable("Instractors");

        builder.HasMany(i => i.Courses)
               .WithOne(c => c.Instructor)
               .HasForeignKey(c => c.InstructorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}