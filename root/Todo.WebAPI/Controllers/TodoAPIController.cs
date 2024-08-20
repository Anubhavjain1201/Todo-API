using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.DAL;
using Todo.Models;

namespace Todo.WebAPI.Controllers
{
    [Route("todo")]
    [ApiController]
    public class TodoAPIController : ControllerBase
    {
        private readonly TodoDbContext _dbContext;
        private readonly ILogger<TodoAPIController> _logger;

        public TodoAPIController(TodoDbContext context, ILogger<TodoAPIController> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            _logger.LogInformation("Fetching all Todo Items");
            dynamic result = await _dbContext.Todos.ToListAsync();

            _logger.LogInformation("Fetched all Todo Items");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            _logger.LogInformation($"Fetching the Todo Item with Id: {id}");

            var item = await _dbContext.Todos.FindAsync(id);

            _logger.LogInformation($"Fetched Todo Item");

            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> InsertItem([FromBody] TodoModel todoItem)
        {
            _logger.LogInformation("Adding the Todo Item");

            _dbContext.Todos.Add(todoItem);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Todo Item added");
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateItem([FromBody] TodoModel todoItem)
        {
            _logger.LogInformation("Updating the Todo Item");

            _dbContext.Entry(todoItem).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Todo Item updated");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            _logger.LogInformation($"Finding Todo Item with Id: {id}");
            var todoItem = await _dbContext.Todos.FindAsync(id);
            if (todoItem == null) return NotFound();

            _logger.LogInformation("Todo Item exists...Proceeding to delete");

            _dbContext.Todos.Remove(todoItem);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Todo Item deleted");
            return Ok();
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}
