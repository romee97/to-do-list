namespace ToDoList.Model
{
    public class ToDoTask
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime TaskDate { get; set; }
        public bool IsDone { get; set; }
        public int Id { get; set; }
    }
}
