namespace PuSocialNetwork.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PollConfig : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder
                .HasMany(p => p.Users)
                .WithOne(u => u.Poll)
                .HasForeignKey(u => u.PollId);
        }
    }
}
