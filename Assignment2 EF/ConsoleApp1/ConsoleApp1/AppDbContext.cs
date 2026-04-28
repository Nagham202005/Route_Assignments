using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=EventHubDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AttendeeConfiguration());

        modelBuilder.Entity<Event>(eb => {
            eb.HasKey(e => e.Id);
            eb.Property(e => e.Title).IsRequired();

            eb.Property<DateTime>("CreatedAt");
            eb.Property<DateTime>("LastModified");
        });

        modelBuilder.Entity<Session>()
            .HasOne(s => s.ParentEvent)
            .WithMany(e => e.Sessions)
            .HasForeignKey(s => s.EventId);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<Badge> Badges { get; set; }
}