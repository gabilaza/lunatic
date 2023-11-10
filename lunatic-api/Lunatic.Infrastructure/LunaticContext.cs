
using Lunatic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lunatic.Infrastructure {
    public class LunaticContext : DbContext {
        public LunaticContext(DbContextOptions<LunaticContext> options) : base(options) {}

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //     optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=db;User Id=student;Password=student");
        // }
    }
}

