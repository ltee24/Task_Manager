using System.ComponentModel.DataAnnotations;

namespace Task_Manager.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        public string TaskName {  get; set; }   
        public string Description { get; set; }
    }
}
