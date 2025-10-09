namespace WebApplication1.DTOs
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
}