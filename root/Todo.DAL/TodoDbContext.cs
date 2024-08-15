using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.DAL
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<TodoModel> Todos => Set<TodoModel>();
    }
}
