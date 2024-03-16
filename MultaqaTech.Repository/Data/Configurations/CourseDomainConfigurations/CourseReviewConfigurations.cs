//namespace MultaqaTech.Repository.Data.Configurations;

//internal class CourseReviewConfigurations : IEntityTypeConfiguration<CourseReview>
//{
//    public void Configure(EntityTypeBuilder<CourseReview> builder)
//    {
//        builder.ToTable("CourseReviews");

//        builder.HasOne(e => e.Course)
//               .WithMany(e => e.Reviews)
//               .HasForeignKey(e => e.CourseId);
//    }
//}