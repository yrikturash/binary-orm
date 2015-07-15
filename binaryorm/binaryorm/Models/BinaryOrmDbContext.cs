using System.Data.Entity;

namespace binaryorm.Models
{
    public class BinaryOrmDbContext : DbContext
    {
        public BinaryOrmDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new BinaryOrmDbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TestWork> TestWorks { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Lecture> Lectures { get; set; }

    }
}