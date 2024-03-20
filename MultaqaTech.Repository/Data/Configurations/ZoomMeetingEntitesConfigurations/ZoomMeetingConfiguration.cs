using MultaqaTech.Core.Entities.ZoomDomainEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultaqaTech.Repository.Data.Configurations.ZoomMeetingEntitesConfigurations
{
    internal class ZoomMeetingConfiguration : IEntityTypeConfiguration<ZoomMeeting>
    {
        public void Configure(EntityTypeBuilder<ZoomMeeting> builder)
        {
            builder.ToTable("ZoomMeetings");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Content)
            .IsRequired();

            builder.Property(e => e.StartDate)
            .IsRequired();

            builder.Property(e => e.Duration)
            .IsRequired();

            builder.Property(e => e.MeetingId)
            .IsRequired();
            

            builder.Property(e => e.ZoomMeetingCategoryId)
             .IsRequired();


            builder.HasOne(bp => bp.Category)
                     .WithMany(c => c.ZoomMeetings)
                     .HasForeignKey(bp => bp.ZoomMeetingCategoryId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Restrict);


           
        }
    }
    
}
