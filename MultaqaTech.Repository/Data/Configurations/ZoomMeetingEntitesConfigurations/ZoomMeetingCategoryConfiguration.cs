namespace MultaqaTech.Repository.Data.Configurations.ZoomMeetingEntitesConfigurations
{
    internal class ZoomMeetingCategoryConfiguration : IEntityTypeConfiguration<ZoomMeetingCategory>
    {
        public void Configure(EntityTypeBuilder<ZoomMeetingCategory> builder)
        {
            builder.ToTable("ZoomMeetingCategories");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
