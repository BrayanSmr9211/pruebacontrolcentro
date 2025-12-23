using Domain.DataNums;

namespace Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus2 Status { get; set; }
        public String? PdfPath { get; set; }
        public DateTime CreatedAT { get; set; }
    }
}
