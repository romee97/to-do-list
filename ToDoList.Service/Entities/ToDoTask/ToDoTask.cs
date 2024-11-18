namespace ToDoList.Service.Entities.ToDoTask
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TaskDate { get; set; }
        public bool IsDone { get; set; }
    }
}
