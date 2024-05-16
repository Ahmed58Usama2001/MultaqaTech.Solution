
namespace MultaqaTech.Repository.Data.Configurations.EventEntitesConfigurations
{
    internal class EventSpeakerConfiguration : IEntityTypeConfiguration<EventSpeaker>
    {
        public void Configure(EntityTypeBuilder<EventSpeaker> builder)
        {
            builder.ToTable("EventSpeakers");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.JobTitle)
                .IsRequired()
                .HasMaxLength(255);

            builder.Ignore(l => l.MediaUrl);

            builder.Property(e => e.EventId)
               .IsRequired();

            builder.HasOne(bp => bp.Event)
                     .WithMany(c => c.Speakers)
                     .HasForeignKey(bp => bp.EventId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
