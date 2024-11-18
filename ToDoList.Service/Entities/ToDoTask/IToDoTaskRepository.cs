namespace ToDoList.Service.Entities.ToDoTask
{
    public interface IToDoTaskRepository
    {
        void Add(ToDoTask task);
        void Update(ToDoTask task);
        void Delete(int[] ids);
    }
}
