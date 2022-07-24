using System.Text.Json.Serialization;
using Dapper.Contrib.Extensions;
using GMS.Data.Models;

namespace GMS.Data.Models
{
    [Table("Gyms")]
    public class Gym
    {
        [Key]
        public int GymId { get; set; }
        public int LocationId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string GymName { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Image {get; set;}
        [JsonIgnore]
        public Location Location { get; set; }
        public IEnumerable<Manager> Managers {get; set;}
        public IEnumerable<Member> Members {get; set;}
        public IEnumerable<Coach> Coaches {get; set;}
    }
}
