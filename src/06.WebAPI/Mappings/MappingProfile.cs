using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Mappings
{
    /// <summary>
    /// Profil AutoMapper untuk mendefinisikan semua pemetaan antara entitas dan DTO.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // === Aturan Mapping untuk Category ===
            
            // Dari Model -> DTO
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.MenuCourseCount, opt => opt.MapFrom(src => src.MenuCourses.Count));

            // Dari DTO -> Model
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();


            // === Aturan Mapping untuk MenuCourse ===

            // Dari Model -> DTO
            CreateMap<MenuCourse, MenuCourseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            // Dari DTO -> Model
            CreateMap<CreateMenuCourseDto, MenuCourse>();
            CreateMap<UpdateMenuCourseDto, MenuCourse>();


            // === Aturan Mapping untuk Schedule ===

            // Dari Model -> DTO
            CreateMap<Schedule, ScheduleDto>();
            // Dari DTO -> Model
            CreateMap<CreateScheduleDto, Schedule>();


            // === Aturan Mapping untuk MenuCourse_Schedule ===
            
            // Dari Model -> DTO
            CreateMap<MenuCourse_Schedule, MenuCourseScheduleDto>()
                .ForMember(dest => dest.MenuCourseName, opt => opt.MapFrom(src => src.MenuCourse.Name))
                .ForMember(dest => dest.ScheduleDate, opt => opt.MapFrom(src => src.Schedule.ScheduleDate));
            
            // Dari DTO -> Model
            CreateMap<CreateMenuCourseScheduleDto, MenuCourse_Schedule>();
            CreateMap<UpdateMenuCourseScheduleDto, MenuCourse_Schedule>();
        }
    }
}