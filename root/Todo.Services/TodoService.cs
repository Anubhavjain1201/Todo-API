using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Todo.DAL.Repositories.Interfaces;
using Todo.Models.DomainModels;
using Todo.Models.DTO;

namespace Todo.Services
{
    public class TodoService : ITodoService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly ILogger<TodoService> _logger;

        public TodoService(IBaseRepository baseRepository, ILogger<TodoService> logger)
        {
            _baseRepository = baseRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task DeleteTodoItem(Guid id)
        {   
            // Check if there is no existing item with the Id
            var existingItem = _baseRepository.GetItemAsync(id);
            if (existingItem == null)
            {
                _logger.LogInformation($"No Todo Task exists with the Id: {id}");
                throw new ValidationException($"No Todo Task exists with the Id: {id}");
            }

            await _baseRepository.DeleteAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TodoDTO?>> GetAllTodoItems()
        {
            return await _baseRepository.GetAllItemsAsync();
        }

        /// <inheritdoc/>
        public async Task<TodoDTO?> GetTodoItem(Guid id)
        {
            return await _baseRepository.GetItemAsync(id);
        }

        /// <inheritdoc/>
        public async Task InsertTodoItem(TodoModel todo)
        {
            // Generate a new Guid for the Todo item
            todo.Id = Guid.NewGuid();
            ValidateTaskName(todo.Task);
            await _baseRepository.InsertAsync(todo);
        }

        /// <inheritdoc/>
        public async Task UpdateTodoItem(TodoModel todo)
        {
            // Check if Id is null
            if(todo.Id == null)
            {
                _logger.LogInformation($"Id cannot be null");
                throw new ValidationException($"Id cannot be null");
            }

            // Check if there is no existing item with the Id
            var existingItem = _baseRepository.GetItemAsync((Guid)todo.Id);
            if (existingItem == null)
            {
                _logger.LogInformation($"No Todo Task exists with the Id: {todo.Id}");
                throw new ValidationException($"No Todo Task exists with the Id: {todo.Id}");
            }

            ValidateTaskName(todo.Task);
            await _baseRepository.UpdateAsync(todo);
        }

        #region Private Methods
        private void ValidateTaskName(string? taskName)
        {
            // Check if Task name is null or empty
            if (string.IsNullOrEmpty(taskName))
            {
                _logger.LogInformation($"Todo task name cannot be null or empty");
                throw new ValidationException($"Todo task name cannot be null or empty");
            }
        }
        #endregion
    }
}
