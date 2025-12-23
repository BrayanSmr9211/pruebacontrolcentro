using Domain.DataNums;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TaskStatus2? status)
        {
            var tasks = await _taskRepository.GetAllTasksAsync(status);
            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                return NotFound(new { message = "Tarea no encontrada" });

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItem task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taskRepository.AddTaskAsync(task);

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskItem updatedTask)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);

            if (existingTask == null)
                return NotFound(new { message = "Tarea no encontrada" });

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.Priority = updatedTask.Priority;
            existingTask.Status = updatedTask.Status;

            await _taskRepository.UpdateTaskAsync(existingTask);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                return NotFound(new { message = "Tarea no encontrada" });

            await _taskRepository.DeleteTaskAsync(id);

            return NoContent();
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var tasks = await _taskRepository.GetAllTasksAsync(null);

            var stats = tasks
                .GroupBy(t => t.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Total = g.Count()
                });

            return Ok(stats);
        }
    }
}
