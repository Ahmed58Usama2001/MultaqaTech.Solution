//namespace MultaqaTech.Repository.Identity;

//public class AppIdentityDbContext : IdentityDbContext<AppUser>
//{
//    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options):
//        base(options)
//    {
        
//    }



//    protected override void OnModelCreating(ModelBuilder builder)
//    {
//        base.OnModelCreating(builder);


//        builder.Entity<AppUser>()
//            .HasOne(u=>u.Instructor)
//            .WithOne(i => i.AppUser)
//            .HasForeignKey<AppUser>(u => u.InstructorId)
//            .OnDelete(DeleteBehavior.Restrict);

//        builder.Entity<AppUser>()
//           .HasOne(u => u.Student)
//           .WithOne(i => i.AppUser)
//           .HasForeignKey<AppUser>(u => u.StudentId)
//           .OnDelete(DeleteBehavior.Restrict);
//    }

//}
