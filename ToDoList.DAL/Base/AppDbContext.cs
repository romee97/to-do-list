using Microsoft.EntityFrameworkCore;
using ToDoList.Service.Entities.ToDoTask;

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
            modelBuilder.Entity<ToDoTask>();
        }
    }
}
