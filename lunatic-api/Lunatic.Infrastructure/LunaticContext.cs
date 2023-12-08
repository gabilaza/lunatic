
using Lunatic.Domain.Entities;
using Lunatic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Task = Lunatic.Domain.Entities.Task;

namespace Lunatic.Infrastructure
{
    public class LunaticContext : DbContext {
        public LunaticContext(DbContextOptions<LunaticContext> options) : base(options) {}

        public DbSet<Comment> Comment { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<User> User { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //     optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=LunaticDB;User Guid=lunatic;Password=lunatic");
        // }
    }
}

