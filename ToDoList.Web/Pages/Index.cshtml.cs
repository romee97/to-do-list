using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using ToDoList.Service.Entities.ToDoTask;

namespace ToDoList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IToDoTaskQueryService toDoTaskQueryService;
        private readonly IToDoTaskRepository toDoTaskRepository;

        public ILookup<DateTime, ToDoTask> Tasks { get; private set; }
        public IEnumerable<IGrouping<DateTime, ToDoTask>> OverdueTasks => Tasks.Where(x => x.Key < DateTime.Today);
        public IEnumerable<IGrouping<DateTime, ToDoTask>> CurrentTasks => Tasks.Where(x => x.Key == DateTime.Today);
        public IEnumerable<IGrouping<DateTime, ToDoTask>> UpcomingTasks => Tasks.Where(x => x.Key > DateTime.Today);

        public IndexModel(ILogger<IndexModel> logger, IToDoTaskQueryService toDoTaskQueryService, IToDoTaskRepository toDoTaskRepository)
        {
            _logger = logger;
            this.toDoTaskQueryService = toDoTaskQueryService;
            this.toDoTaskRepository = toDoTaskRepository;
        }

        public void OnGet()
        {
            FetchData();
        }

        public void FetchData()
            => Tasks = toDoTaskQueryService.GetAll(done: false)
                                           .ToLookup(t => t.TaskDate);
    }
}
