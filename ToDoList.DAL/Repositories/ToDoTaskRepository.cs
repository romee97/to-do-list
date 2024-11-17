using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Base;
using ToDoList.Model.Entities.ToDoTask;

namespace ToDoList.DAL.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly IAppDbContext dbContext;
        private readonly IToDoTaskValidator validator;

        private DbSet<ToDoTask> DbSet => dbContext.Set<ToDoTask>();

        public ToDoTaskRepository(IAppDbContext dbContext, IToDoTaskValidator validator)
        {
            this.dbContext = dbContext;
            this.validator = validator;
        }

        public async Task Add(ToDoTask task)
        {
            await validator.ValidateWrite(task);

            DbSet.Add(task);
        }

        public async Task Delete(int[] ids)
        {
            var recordsToDelete = await Get(ids);

            validator.ValidateDelete(ids, recordsToDelete);

            DbSet.RemoveRange(recordsToDelete);
        }

        public async Task Update(ToDoTask task)
        {
            await validator.ValidateWrite(task);

            var taskInDatabase = await Get(task.Id);

            taskInDatabase.Title = task.Title;
            taskInDatabase.Description = task.Description;
            taskInDatabase.TaskDate = task.TaskDate;
            taskInDatabase.IsDone = task.IsDone;

            DbSet.Update(taskInDatabase);
        }

        private Task<ToDoTask> Get(int id)
            => DbSet.SingleAsync(t => t.Id == id);

        private Task<List<ToDoTask>> Get(IEnumerable<int> ids)
            => DbSet.Where(t => ids.Contains(t.Id))
                    .ToListAsync();
    }
}
