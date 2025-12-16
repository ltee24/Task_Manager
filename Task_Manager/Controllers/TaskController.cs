using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Task_Manager.Data;
using Task_Manager.Models;
using Task_Manager.Models.DTO;
using Task_Manager.Models.Enums;

namespace Task_Manager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        private ResponseDTO _response;

        public TaskController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDTO();
        }

        [HttpGet]
        public ResponseDTO Index([FromQuery(Name ="filterCategory")]int? category, [FromQuery(Name ="filterTaskState")]int? taskState)
        {
            try
            {
                //builds query
                var query = _db.Tasks.AsQueryable();
                if (category > 0)
                {
                    query = query.Where(t => t.Category == (TaskCategory)category);
                }
                if (taskState > 0)
                {
                    query = query.Where(t => t.Status == (TaskState)taskState);
                }
                var tasks = query.ToList();
                _response.Result = _mapper.Map<IEnumerable<TaskItemDTO>>(tasks);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }
        [HttpGet]
        [Route("GetById/{id:int}")]
        public ResponseDTO GetById(int id)
        {
            try
            {
                TaskItem task = _db.Tasks.FirstOrDefault(t => t.Id == id);
                _response.Result = _mapper.Map<TaskItemDTO>(task);
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDTO Create([FromBody] TaskItemDTO task)
        {
            try
            {
            
                    TaskItem dbTask = _mapper.Map<TaskItem>(task);
                    _db.Add(dbTask);
                    _db.SaveChanges();
                    _response.Result = _mapper.Map<TaskItemDTO>(dbTask);
                   
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDTO Update([FromBody] TaskItemDTO task)
        {
            try
            {
                TaskItem dbTask = _mapper.Map<TaskItem>(task);
                _db.Update(dbTask);
                _db.SaveChanges();
                _response.Result = _mapper.Map<TaskItemDTO>(dbTask);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }

        [HttpDelete]
        public ResponseDTO Delete(int id)
        {
            try
            {
                TaskItem task = _db.Tasks.First(t=>t.Id == id);
                _db.Remove(task);
                _db.SaveChanges();
            }
            catch(Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
