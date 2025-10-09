using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Mappings
{
    /// <summary>
    /// AutoMapper profile untuk semua pemetaan antara entitas dan DTO di aplikasi.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Mengonfigurasi semua mapping yang dibutuhkan.
        /// </summary>
        public MappingProfile()
        {
            // ========== Category Mappings ==========
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.MenuCourseCount, opt => opt.MapFrom(src => src.MenuCourses.Count));
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // ========== MenuCourse Mappings ==========
            CreateMap<MenuCourse, MenuCourseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<CreateMenuCourseDto, MenuCourse>();
            CreateMap<UpdateMenuCourseDto, MenuCourse>();

            // ========== Schedule Mappings ==========
            CreateMap<Schedule, ScheduleDto>();
            CreateMap<CreateScheduleDto, Schedule>();

            // ========== MenuCourse_Schedule Mappings ==========
            CreateMap<MenuCourse_Schedule, MenuCourseScheduleDto>()
                .ForMember(dest => dest.MenuCourseName, opt => opt.MapFrom(src => src.MenuCourse.Name))
                .ForMember(dest => dest.ScheduleDate, opt => opt.MapFrom(src => src.Schedule.ScheduleDate));
            CreateMap<CreateMenuCourseScheduleDto, MenuCourse_Schedule>();
            CreateMap<UpdateMenuCourseScheduleDto, MenuCourse_Schedule>();
        }
    }
}