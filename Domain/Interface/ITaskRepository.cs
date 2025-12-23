using Domain.DataNums;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync(TaskStatus2? status);
        Task<TaskItem?> GetByIdAsync(int id);
        Task AddTaskAsync(TaskItem task);
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int id);
    }
}