using System.Text.Json.Serialization;

namespace GMS.Data.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        [JsonIgnore]
        public Coach Coach { get; set; }
        public int CoachId { get; set; }
        public IEnumerable<Session> Sessions {get; set;}
    }
}
//я хочу ходить к этому тренеру на индивидуальные тренировки по понедельникам средам пятницам в 16:00 