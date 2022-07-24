using System.ComponentModel.DataAnnotations;

namespace GMS.Data.Models
{
    public class MembershipType
    {
        public int MembershipTypeId { get; set; }
        [Required]
        public string MembershipTypeName { get; set; }
    }
}