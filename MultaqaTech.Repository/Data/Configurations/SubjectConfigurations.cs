namespace MultaqaTech.Repository.Data.Configurations;

internal class SubjectConfigurations : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.ToTable("Subjects");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
