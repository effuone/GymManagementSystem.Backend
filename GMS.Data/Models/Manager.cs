using System.Text.Json.Serialization;
using Dapper.Contrib.Extensions;
using GMS.Data.Models;

namespace GMS.Data.Models
{
    [Table("Managers")]
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string GMSUserId { get; set; }
        [JsonIgnore]
        public ManagerType ManagerType { get; set; }
        public int ManagerTypeId { get; set; }
        public Gym Gym {get; set;}
    }
}
