using System.ComponentModel.DataAnnotations;

namespace GMS.Data.AuthModels
{
    public class LoginModelType
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}