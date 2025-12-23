using Domain.DataNums;
using Domain.Entities;
using Domain.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync(TaskStatus2? status)
        {
            IQueryable<TaskItem> query = _context.Tasks.AsNoTracking();

            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
