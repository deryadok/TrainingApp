using AutoMapper;
using TrainingApp.Application.Dtos;
using TrainingApp.Domain.Entities;

namespace TrainingApp.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Workout, WorkoutDto>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
