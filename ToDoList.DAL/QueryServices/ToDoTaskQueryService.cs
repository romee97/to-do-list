using Microsoft.EntityFrameworkCore;
using ToDoList.DAL.Base;
using ToDoList.Model.Entities.ToDoTask;

namespace ToDoList.DAL.QueryServices
{
    public class ToDoTaskQueryService : IToDoTaskQueryService
    {
        private readonly IAppDbContext dbContext;

        private IQueryable<ToDoTask> Query => dbContext.Set<ToDoTask>().AsNoTracking();

        public ToDoTaskQueryService(IAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<ToDoTask>> Get(DateTime dateFrom, DateTime dateTo)
            => await Query.Where(t => t.TaskDate >= dateFrom
                                   && t.TaskDate <= dateTo)
                          .ToListAsync();

        public Task<ToDoTask?> TryGet(string title, DateTime date)
            => Query.SingleOrDefaultAsync(t => t.Title == title
                                            && t.TaskDate == date.Date);
    }
}
