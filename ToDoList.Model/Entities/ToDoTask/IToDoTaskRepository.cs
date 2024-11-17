namespace ToDoList.Model.Entities.ToDoTask
{
    public interface IToDoTaskRepository
    {
        Task Add(ToDoTask task);
        Task Update(ToDoTask task);
        Task Delete(int[] ids);
    }
}
