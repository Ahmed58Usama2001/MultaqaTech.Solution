
namespace MultaqaTech.Repository.Data.Configurations.EventEntitesConfigurations
{
    internal class EventCommentConfiguration : IEntityTypeConfiguration<EventComment>
    {
        public void Configure(EntityTypeBuilder<EventComment> builder)
        {
            builder.ToTable("EventComments");

            builder.Property(e => e.CommentContent)
                .IsRequired();

            builder.Property(e => e.DatePosted)
                .IsRequired();

            builder.Property(e => e.EventId)
                .IsRequired();

            builder.HasOne(bp => bp.Event)
                     .WithMany(c => c.Comments)
                     .HasForeignKey(bp => bp.EventId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
