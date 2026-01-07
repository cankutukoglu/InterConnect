using LinkedInClone.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkedInClone.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<Education> Educations => Set<Education>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Achievement> Achievements => Set<Achievement>();
    public DbSet<Language> Languages => Set<Language>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>().HasIndex(u => u.Email).IsUnique();

        b.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne()
            .HasForeignKey<Profile>(p => p.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        b.Entity<Profile>()
            .HasIndex(p => p.UserId)
            .IsUnique();

        b.Entity<Experience>()
            .ToTable(t => t.HasCheckConstraint(
                "experience_date_check",
                "StartDate < EndDate OR EndDate IS NULL"));

        b.Entity<Experience>()
            .HasOne<User>()
            .WithMany(u => u.Experiences)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Entity<Education>()
            .HasOne<User>()
            .WithMany(u => u.Educations)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Entity<Education>()
            .ToTable(t => t.HasCheckConstraint(
                "education_date_check",
                "StartYear < EndYear"));

        b.Entity<Skill>()
            .HasOne<User>()
            .WithMany(u => u.Skills)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Entity<Achievement>()
            .HasOne<User>()
            .WithMany(u => u.Achievements)
            .HasForeignKey(h => h.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Entity<Language>()
            .HasOne<User>()
            .WithMany(u => u.Languages)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
