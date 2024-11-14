namespace ToDoList.Model
{
    public interface IToDoTaskRepository
    {
        Task<IReadOnlyCollection<ToDoTask>> Get(DateTime? dateFrom, DateTime? dateTo);
        Task<ToDoTask> Get(int id);
        void Add(ToDoTask task);
        void Update(ToDoTask task);
        void Delete(int[] ids);
    }
}
