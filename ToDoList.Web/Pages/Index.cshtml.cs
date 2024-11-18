using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Service.Entities.ToDoTask;

namespace ToDoList.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IToDoTaskQueryService toDoTaskQueryService;
        private readonly IToDoTaskRepository toDoTaskRepository;

        public DateTime DateFilter { get; set; } = DateTime.Today;
        public ILookup<bool, ToDoTask> Tasks { get; private set; }
        public IEnumerable<ToDoTask> DoneTasks => Tasks[true];
        public IEnumerable<ToDoTask> TasksToDo => Tasks[false];

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

        public void OnPostDateChanged(DateChangedParams dateChangedParams)
        {
            DateFilter = dateChangedParams.TaskDate;
            FetchData();
        }

        public void FetchData()
            => Tasks = toDoTaskQueryService.Get(DateFilter)
                                           .ToLookup(t => t.IsDone);

        public class DateChangedParams
        {
            public DateTime TaskDate { get; set; }
        }
    }
}
