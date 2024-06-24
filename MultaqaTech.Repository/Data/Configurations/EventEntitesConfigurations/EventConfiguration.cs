
namespace MultaqaTech.Repository.Data.Configurations.EventEntitesConfigurations
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.Property(e => e.Title)
               .IsRequired()
               .HasMaxLength(255);

            builder.Property(e => e.AboutTheEvent)
            .IsRequired();

            builder.Property(e => e.DateFrom)
            .IsRequired();

            builder.Property(e => e.DateTo)
            .IsRequired();

            builder.Property(e => e.TimeFrom)
            .IsRequired();

            builder.Property(e => e.TimeTo)
            .IsRequired();

            builder.Property(e => e.Price)
                .IsRequired();

            builder.Property(e => e.Address)
               .IsRequired();

            builder.Property(e => e.PhoneNumber)
               .IsRequired();

            builder.Property(e => e.Website)
               .IsRequired();

            builder.Property(e => e.EventCategoryId)
             .IsRequired();

            builder.Ignore(l => l.MediaUrl);

            builder.HasOne(bp => bp.Category)
                     .WithMany(c => c.Events)
                     .HasForeignKey(bp => bp.EventCategoryId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bp => bp.Country)
                     .WithMany(c => c.Events)
                     .HasForeignKey(bp => bp.EventCountryId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(bp => bp.Comments)
               .WithOne(c => c.Event)
               .HasForeignKey(c => c.EventId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(bp => bp.Speakers)
              .WithOne(c => c.Event)
              .HasForeignKey(c => c.EventId)
              .OnDelete(DeleteBehavior.Cascade);




        }
    }
}
