using Microsoft.EntityFrameworkCore;
using TodoApp.Model;

namespace TodoApp.Data
    {
    public class TodoContext(DbContextOptions<TodoContext> options):DbContext(options)
        {
        public DbSet<Todo> todos  { get; set; }
        }
    }
