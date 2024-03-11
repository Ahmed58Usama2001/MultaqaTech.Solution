namespace MultaqaTech.Repository.Data.Configurations;

internal class CoursesTagsConfigurations : IEntityTypeConfiguration<CourseTag>
{
    public void Configure(EntityTypeBuilder<CourseTag> builder)
    {
        builder.ToTable("CoursesTags");

        builder.HasKey(e => new { e.TagId, e.CourseId });

        builder.HasOne(e => e.Course)
               .WithMany(e => e.Tags)
               .HasForeignKey(e => e.CourseId);
    }
}