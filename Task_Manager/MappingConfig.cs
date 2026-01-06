using AutoMapper;
using Task_Manager.Models;
using Task_Manager.Models.DTO;

namespace Task_Manager
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<TaskItemDTO, TaskItem>();
                config.CreateMap<TaskItem, TaskItemDTO>();
                config.CreateMap<ApplicationUser,UserDTO>();
            });
            return mappingConfig;
        }
    }
}
