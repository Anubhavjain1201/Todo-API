using Todo.Models.DomainModels;
using Todo.Models.DTO;

namespace Todo.Services
{
    public interface ITodoService
    {
        /// <summary>
        /// Inserts a Todo Item
        /// </summary>
        /// <param name="todo"></param>
        Task InsertTodoItem(TodoModel todo);

        /// <summary>
        /// Updates a Todo Item
        /// </summary>
        /// <param name="todo"></param>
        Task UpdateTodoItem(TodoModel todo);

        /// <summary>
        /// Deletes a Todo Item
        /// </summary>
        /// <param name="id"></param>
        Task DeleteTodoItem(Guid id);

        /// <summary>
        /// Gets Todo Item by Id
        /// </summary>
        /// <param name="id"></param>
        Task<TodoDTO?> GetTodoItem(Guid id);

        /// <summary>
        /// Gets all todo items
        /// </summary>
        Task<IEnumerable<TodoDTO?>> GetAllTodoItems();
    }
}
