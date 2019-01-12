namespace PuSocialNetwork.Data
{
    using EntityConfig;
    using Models;
    using Microsoft.EntityFrameworkCore;

    public class SocialNetworkDbContext : DbContext
    {
        public SocialNetworkDbContext(DbContextOptions<SocialNetworkDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new UserConfig());

            base.OnModelCreating(builder);
        }
    }
}
