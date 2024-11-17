using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Base;
using ToDoList.Model.Entities.ToDoTask;

namespace ToDoList.DAL.QueryServices
{
    public class ToDoTaskQueryService : IToDoTaskQueryService
    {
        private readonly AppDbContext dbContext;

        private IQueryable<ToDoTask> Query => dbContext.Set<ToDoTask>().AsNoTracking();

        public ToDoTaskQueryService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IReadOnlyCollection<ToDoTask> Get(DateTime dateFrom, DateTime dateTo)
            => Query.Where(t => t.TaskDate >= dateFrom
                             && t.TaskDate <= dateTo)
                    .ToList();

        public ToDoTask? TryGet(string title, DateTime date)
            => Query.SingleOrDefault(t => t.Title == title
                                       && t.TaskDate == date.Date);
    }
}
