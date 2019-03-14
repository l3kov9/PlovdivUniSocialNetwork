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

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Play2048Score> Play2048Scores { get; set; }

        public DbSet<Poll> Polls { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new UserConfig());

            builder
                .ApplyConfiguration(new PostConfig());

            builder
                .ApplyConfiguration(new PollConfig());

            builder
                .Entity<UserPoll>()
                .HasKey(up => new { up.UserId, up.PollId });

            //builder
            //    .Entity<Comment>()
            //    .HasKey(c => new { c.PostId, c.UserId });

            //builder
            //    .Entity<Like>()
            //    .HasKey(l => new { l.PostId, l.UserId });

            base.OnModelCreating(builder);
        }
    }
}
