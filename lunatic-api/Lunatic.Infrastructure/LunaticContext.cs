
using Lunatic.Domain.Entities;
using Lunatic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Task = Lunatic.Domain.Entities.Task;

namespace Lunatic.Infrastructure
{
    public class LunaticContext : DbContext {
        public LunaticContext(DbContextOptions<LunaticContext> options) : base(options) {}

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //     optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=LunaticDB;User Guid=lunatic;Password=lunatic");
        // }
    }
}

