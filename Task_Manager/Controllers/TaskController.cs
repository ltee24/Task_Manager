using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Task_Manager.Data;
using Task_Manager.Models;

namespace Task_Manager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;

        public TaskController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<TaskItem> tasks = _db.Tasks.ToList();
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskItem task)
        {
            _db.Add(task);  
            _db.SaveChanges();
            return Ok();
        }
    }
}
