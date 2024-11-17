using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Base;
using ToDoList.Model.Entities.ToDoTask;

namespace ToDoList.DAL.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IToDoTaskValidator validator;

        private DbSet<ToDoTask> DbSet => dbContext.Set<ToDoTask>();

        public ToDoTaskRepository(AppDbContext dbContext, IToDoTaskValidator validator)
        {
            this.dbContext = dbContext;
            this.validator = validator;
        }

        public void Add(ToDoTask task)
        {
            validator.ValidateWrite(task);

            DbSet.Add(task);
        }

        public void Delete(int[] ids)
        {
            var recordsToDelete = Get(ids);

            validator.ValidateDelete(ids, recordsToDelete);

            DbSet.RemoveRange(recordsToDelete);
        }

        public void Update(ToDoTask task)
        {
            validator.ValidateWrite(task);

            var taskInDatabase = Get(task.Id);

            taskInDatabase.Title = task.Title;
            taskInDatabase.Description = task.Description;
            taskInDatabase.TaskDate = task.TaskDate;
            taskInDatabase.IsDone = task.IsDone;

            DbSet.Update(taskInDatabase);
        }

        private ToDoTask Get(int id)
            => DbSet.Single(t => t.Id == id);

        private List<ToDoTask> Get(IEnumerable<int> ids)
            => DbSet.Where(t => ids.Contains(t.Id))
                    .ToList();
    }
}
