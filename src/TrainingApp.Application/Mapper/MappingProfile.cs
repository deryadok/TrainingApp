using AutoMapper;
using TrainingApp.Application.Dtos.Exercise;
using TrainingApp.Application.Dtos.Region;
using TrainingApp.Application.Dtos.User;
using TrainingApp.Application.Dtos.Workout;
using TrainingApp.Domain.Entities;

namespace TrainingApp.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Workout, WorkoutInsertDto>().ReverseMap();
            CreateMap<Workout, WorkoutUpdateDto>().ReverseMap();
            CreateMap<Workout, WorkoutDto>().ReverseMap();

            CreateMap<Region, RegionInsertDto>().ReverseMap();
            CreateMap<Region, RegionUpdateDto>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();

            CreateMap<User, UserInsertDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();


            CreateMap<Exercise, ExerciseInsertDto>().ReverseMap();
            CreateMap<Exercise, ExerciseUpdateDto>().ReverseMap();
            CreateMap<Exercise, ExerciseDto>().ReverseMap();
        }
    }
}
