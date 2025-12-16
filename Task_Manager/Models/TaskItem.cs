using System.ComponentModel.DataAnnotations;
using Task_Manager.Models.Enums;

namespace Task_Manager.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        public string Title {  get; set; }   
        public string Description { get; set; }
        public TaskCategory Category { get; set; }
        public TaskState Status { get; set; }
    }
}
