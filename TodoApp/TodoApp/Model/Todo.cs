namespace TodoApp.Model
    {
    public class Todo
        {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Completed { get; set; }
        }
    }
