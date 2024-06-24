

namespace MultaqaTech.Repository.Data.Configurations.EventEntitesConfigurations
{
    internal class EventCountryConfiguration : IEntityTypeConfiguration<EventCountry>
    {
        public void Configure(EntityTypeBuilder<EventCountry> builder)
        {
            builder.ToTable("EventCountries");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
