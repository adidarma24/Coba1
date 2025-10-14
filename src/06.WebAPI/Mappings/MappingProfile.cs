using AutoMapper;
using MyApp.WebAPI.DTOs;
using MyApp.WebAPI.Models;

namespace MyApp.WebAPI.Mappings
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      // === Pemetaan untuk Invoice ===
      CreateMap<Invoice, InvoiceDTO>();

      CreateMap<Invoice, DetailInvoiceDTO>()
          .ForMember(dest => dest.ListCourse, opt => opt.MapFrom(src => src.InvoiceMenuCourses));

      CreateMap<InvoiceMenuCourse, CourseItemDTO>()
          .ForMember(dest => dest.MenuCourseId, opt => opt.MapFrom(src => src.MenuCourseSchedule.MenuCourse.MenuCourseId))
          .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MenuCourseSchedule.MenuCourse.Name))
          .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.MenuCourseSchedule.MenuCourse.Category.Name))
          .ForMember(dest => dest.ScheduleDate, opt => opt.MapFrom(src => src.MenuCourseSchedule.Schedule.ScheduleDate))
          .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.MenuCourseSchedule.MenuCourse.Price));

      // === Pemetaan untuk MyClass ===
      CreateMap<MyClass, MyClassDTO>()
          .ForMember(dest => dest.MenuCourseId, opt => opt.MapFrom(src => src.MenuCourseSchedule.MenuCourse.MenuCourseId))
          .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MenuCourseSchedule.MenuCourse.Name))
          .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.MenuCourseSchedule.MenuCourse.Image))
          .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.MenuCourseSchedule.MenuCourse.Category.Name))
          .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.MenuCourseSchedule.Schedule.ScheduleDate));

      // === Pemetaan untuk Category ===
      CreateMap<Category, CategoryDto>()
          .ForMember(dest => dest.MenuCourseCount, opt => opt.MapFrom(src => src.MenuCourses.Count));

      // === Pemetaan untuk MenuCourse ===
      CreateMap<MenuCourse, MenuCourseDto>()
          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MenuCourseId))
          .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

      // === Pemetaan untuk Schedule ===
      CreateMap<Schedule, ScheduleDto>();
      CreateMap<CreateScheduleDto, Schedule>();
      CreateMap<UpdateScheduleDto, Schedule>();

      // === Pemetaan untuk MenuCourseSchedule ===
      CreateMap<MenuCourseSchedule, MenuCourseScheduleDto>()
          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MSId))
          .ForMember(dest => dest.MenuCourseName, opt => opt.MapFrom(src => src.MenuCourse.Name))
          .ForMember(dest => dest.ScheduleDate, opt => opt.MapFrom(src => src.Schedule.ScheduleDate));
    }
  }
}