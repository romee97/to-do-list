namespace ToDoList.Model.Entities.ToDoTask
{
    public interface IToDoTaskQueryService
    {
        IReadOnlyCollection<ToDoTask> Get(DateTime dateFrom, DateTime dateTo);
        ToDoTask? TryGet(string title, DateTime date);
    }
}
