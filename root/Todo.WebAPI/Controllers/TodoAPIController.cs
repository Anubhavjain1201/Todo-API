using Microsoft.AspNetCore.Mvc;
using Todo.Models.DomainModels;
using Todo.Services;

namespace Todo.WebAPI.Controllers
{
    [Route("todo")]
    [ApiController]
    public class TodoAPIController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodoAPIController> _logger;

        public TodoAPIController(ITodoService todoService, ILogger<TodoAPIController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            _logger.LogInformation("Fetching all Todo Items");
            dynamic result = await _todoService.GetAllTodoItems();
            _logger.LogInformation("Fetched all Todo Items");
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            _logger.LogInformation($"Fetching the Todo Item with Id: {id}");
            var item = await _todoService.GetTodoItem(id);
            _logger.LogInformation($"Fetched Todo Item");

            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> InsertItem([FromBody] TodoModel todoItem)
        {
            _logger.LogInformation("Adding the Todo Item");
            await _todoService.InsertTodoItem(todoItem);
            _logger.LogInformation("Todo Item added");
            
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateItem([FromBody] TodoModel todoItem)
        {
            _logger.LogInformation("Updating the Todo Item");
            await _todoService.UpdateTodoItem(todoItem);
            _logger.LogInformation("Todo Item updated");
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            _logger.LogInformation("Deleting the Todo Item");
            await _todoService.DeleteTodoItem(id);
            _logger.LogInformation("Todo Item deleted");
            
            return Ok();
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            _logger.LogInformation("Health Check");
            return Ok();
        }
    }
}
