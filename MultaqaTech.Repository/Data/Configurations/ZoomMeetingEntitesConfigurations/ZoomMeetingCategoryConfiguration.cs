using MultaqaTech.Core.Entities.ZoomDomainEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
