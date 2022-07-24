using System.ComponentModel.DataAnnotations;

namespace GMS.Data.Models
{
    public class CoachType
    {
        public int CoachTypeId { get; set; }
        [Required]
        public string CoachTypeName { get; set; }
    }
}