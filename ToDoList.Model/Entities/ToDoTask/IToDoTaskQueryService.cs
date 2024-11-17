namespace ToDoList.Model.Entities.ToDoTask
{
    public interface IToDoTaskQueryService
    {
        Task<IReadOnlyCollection<ToDoTask>> Get(DateTime dateFrom, DateTime dateTo);
        Task<ToDoTask?> TryGet(string title, DateTime date);
    }
}
