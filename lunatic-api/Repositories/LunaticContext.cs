
using Microsoft.EntityFrameworkCore;

namespace Repositories {
    public class LunaticContext : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=db;User Id=student;Password=student");
        }
    }
}

