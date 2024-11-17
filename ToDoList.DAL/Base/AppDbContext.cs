using Microsoft.EntityFrameworkCore;
using ToDoList.Model.Entities.ToDoTask;

namespace ToDoList.DAL.Base
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoTask>()
                        .HasMany(e => e.Predecessors)
                        .WithMany(e => e.Successors);
        }
    }
}
