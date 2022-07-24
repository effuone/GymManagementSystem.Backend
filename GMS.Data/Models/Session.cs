using System.Text.Json.Serialization;

namespace GMS.Data.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        [JsonIgnore]
        public Member Member { get; set; }
        public int MemberId { get; set; }
        [JsonIgnore]
        public Coach Coach { get; set; }
        public int CoachId { get; set; }
        public Schedule Schedule { get; set; }
    }
}