using System.Text.Json.Serialization;
using Dapper.Contrib.Extensions;
using GMS.Data.Models;

namespace GMS.Data.Models
{
    [Table("Members")]
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string GMSUserId { get; set; }
        [JsonIgnore]
        public MembershipType MembershipType { get; set; }
        public int MembershipTypeId { get; set; }
        [JsonIgnore]
        public Gym Gym {get; set;}
        public int GymId { get; set; }
        [JsonIgnore]
        public Coach Coach {get; set;}
        public IEnumerable<Session> Sessions {get; set;}
    }
}
