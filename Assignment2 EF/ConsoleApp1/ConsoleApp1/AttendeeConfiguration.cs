using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
{
    public void Configure(EntityTypeBuilder<Attendee> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.FullName).IsRequired().HasMaxLength(100);

        builder.HasOne(a => a.Badge)
               .WithOne(b => b.Attendee)
               .HasForeignKey<Badge>(b => b.AttendeeId);
    }
}