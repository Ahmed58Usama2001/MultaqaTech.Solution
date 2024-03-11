namespace MultaqaTech.Repository.Data.Configurations;

internal class CoursesPrerequestsConfigurations : IEntityTypeConfiguration<CoursePrerequist>
{
    public void Configure(EntityTypeBuilder<CoursePrerequist> builder)
    {
        builder.ToTable("CoursesPrerequists");

        builder.HasKey(e => new { e.PrerequistId, e.CourseId });

        builder.HasOne(e => e.Course)
               .WithMany(e => e.Prerequisites)
               .HasForeignKey(e => e.CourseId);
    }
}