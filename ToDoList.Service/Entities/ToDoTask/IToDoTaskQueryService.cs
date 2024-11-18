namespace ToDoList.Service.Entities.ToDoTask
{
    public interface IToDoTaskQueryService
    {
        IReadOnlyCollection<ToDoTask> GetAll(bool done);
        IReadOnlyCollection<ToDoTask> Get(DateTime date);
        ToDoTask? TryGet(string title, DateTime date);
    }
}
