using Microsoft.EntityFrameworkCore;
using ToDoList.Model.Entities.ToDoTask;

namespace ToDoList.DAL.Base
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly string connectionString;

        public AppDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoTask>()
                        .HasMany(e => e.Predecessors)
                        .WithMany(e => e.Successors);
        }
    }
}
