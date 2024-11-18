namespace ToDoList.Service.Entities.ToDoTask
{
    public interface IToDoTaskValidator
    {
        void ValidateDelete(ICollection<int> ids, ICollection<ToDoTask> foundRecords);
        void ValidateWrite(ToDoTask task);
    }
}