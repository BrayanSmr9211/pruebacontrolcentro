using Domain.DataNums;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Microsoft.Build.Utilities.Task;

namespace Domain.Interface
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllTasksAsync(TaskStatus2? status);
       // Task<Dictionary<TaskStatus, int>> GetTasksAsync();
        Task<TaskItem> GetByIdAsync(int id);
        Task AddTaskAsync(TaskItem task);
        Task DeleteTaskAsync(TaskItem task);
    }
}
