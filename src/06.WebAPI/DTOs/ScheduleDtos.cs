// Lokasi: src/06.WebAPI/DTOs/ScheduleDtos.cs

namespace MyApp.WebAPI.DTOs
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public DateTime ScheduleDate { get; set; }
    }

    public class CreateScheduleDto
    {
        public DateTime ScheduleDate { get; set; }
    }

    public class UpdateScheduleDto
    {
        public DateTime ScheduleDate { get; set; }
    }
}