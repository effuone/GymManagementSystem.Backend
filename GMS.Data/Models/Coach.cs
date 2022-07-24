using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GMS.Data.Models
{
    [Table("Coaches")]
    public class Coach
    {
        [Key]
        public int CoachId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string GMSUserId { get; set; }
        [JsonIgnore]
        public CoachType CoachType { get; set; }
        public int CoachTypeId { get; set; }
        [JsonIgnore]
        public Gym Gym {get; set;}
        public IEnumerable<Member> Members { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
    }
}