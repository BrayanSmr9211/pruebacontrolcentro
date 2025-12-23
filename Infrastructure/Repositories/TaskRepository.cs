using Domain.DataNums;
using Domain.Entities;
using Domain.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class TaskRepository: ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task <IEnumerable<TaskItem>> GetAllTasksAsync(TaskStatus2? status)
        {
            IQueryable<TaskItem> query = (IQueryable<TaskItem>)_context.Tasks.AsNoTracking();

            //var query = _context.Tasks.AsNoTracking();

            if (status.HasValue )
            {
                query = query.Where(t => t.Status == status.Value);
            }


            return await query.ToListAsync();
        }

     
        public async Task<IEnumerable<TaskItem>> GetByIdAsync(int id)
        {
            var result = await _context.Tasks.FindAsync(id);

            if (result == null)
            {
                throw new InvalidOperationException ("error");
            }
            return (IEnumerable<TaskItem>)result;

        }

        public async Task AddTaskAsync (TaskItem task )
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync (TaskItem task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

    }
}
