namespace MultaqaTech.Repository.Data.Configurations;

internal class TagConfigurations : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
