using Microsoft.EntityFrameworkCore;

namespace PersoTypeAPIs.Models
{
    public class PersoTypeDbContext : DbContext
    {
        public PersoTypeDbContext(DbContextOptions<PersoTypeDbContext> options) : base(options)
        {

        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
