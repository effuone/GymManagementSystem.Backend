using System.ComponentModel.DataAnnotations;

namespace GMS.Data.Models
{
    public class ManagerType
    {
        public int ManagerTypeId { get; set; }
        [Required]
        public string ManagerTypeName { get; set; }
    }
}