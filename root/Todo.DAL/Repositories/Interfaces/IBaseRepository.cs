using Todo.Models.DomainModels;
using Todo.Models.DTO;

namespace Todo.DAL.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        /// <summary>
        /// Inserts a Todo Item
        /// </summary>
        /// <param name="todo"></param>
        Task<dynamic> InsertAsync(TodoModel todo);

        /// <summary>
        /// Updates a Todo Item
        /// </summary>
        /// <param name="todo"></param>
        Task<dynamic> UpdateAsync(TodoModel todo);

        /// <summary>
        /// Deletes a Todo Item specified by its <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        Task<dynamic> DeleteAsync(Guid id);

        /// <summary>
        /// Gets all Todo Items
        /// </summary>
        Task<IEnumerable<TodoDTO?>> GetAllItemsAsync();

        /// <summary>
        /// Gets a Todo Item specified by its <paramref name="id"/>. Returns null, if no item found
        /// </summary>
        /// <param name="id"></param>
        Task<TodoDTO?> GetItemAsync(Guid id);
    }
}
