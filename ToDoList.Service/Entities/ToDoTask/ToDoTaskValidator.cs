namespace ToDoList.Service.Entities.ToDoTask
{
    public class ToDoTaskValidator : IToDoTaskValidator
    {
        private readonly IToDoTaskQueryService toDoTaskQueryService;

        public ToDoTaskValidator(IToDoTaskQueryService toDoTaskQueryService)
        {
            this.toDoTaskQueryService = toDoTaskQueryService;
        }

        public void ValidateWrite(ToDoTask task)
        {
            var recordInDatabase = toDoTaskQueryService.TryGet(task.Title, task.TaskDate);

            if (recordInDatabase is not null && recordInDatabase.Id != task.Id)
                throw new InvalidDataException($"Another task named '{task.Title}' has already been added for date {task.TaskDate:d}");
        }

        public void ValidateDelete(ICollection<int> ids, ICollection<ToDoTask> foundRecords)
        {
            var foundIds = foundRecords.Select(t => t.Id).ToHashSet();

            if (!foundIds.SetEquals(ids))
                throw new InvalidDataException($"An error occurred while deleting tasks");
        }
    }
}
