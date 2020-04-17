using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCalendar.DAL.Models.Entities;

namespace WebCalendar.DAL.EF.Configurations
{
    public class PushSubscriptionConfiguration : IEntityTypeConfiguration<PushSubscription>
    {
        public void Configure(EntityTypeBuilder<PushSubscription> builder)
        {
            builder.HasOne(p => p.User)
                .WithOne(u => u.PushSubscription)
                .HasForeignKey<User>(u => u.PushSubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}