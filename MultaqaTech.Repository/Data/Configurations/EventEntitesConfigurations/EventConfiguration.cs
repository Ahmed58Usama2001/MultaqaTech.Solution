
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

            builder.Property(e => e.Content)
            .IsRequired();

            builder.Property(e => e.Country)
            .IsRequired();

            builder.Property(e => e.StartDate)
            .IsRequired();

            builder.Property(e => e.From)
            .IsRequired();

            builder.Property(e => e.To)
            .IsRequired();

            builder.Property(e => e.EventCategoryId)
             .IsRequired();

            builder.Ignore(l => l.MediaUrl);

            builder.HasOne(bp => bp.Category)
                     .WithMany(c => c.Events)
                     .HasForeignKey(bp => bp.EventCategoryId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Price).HasColumnType("decimal(18,2)");

        }
    }
}
