using Microsoft.AspNetCore.Http;

namespace GMS.Data.DTOs
{
    public class GymDTO
    {
        public int LocationId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string GymName { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string ImageFilePath {get; set;}
    }
    public class CreateGymDTO
    {
        public int LocationId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string GymName { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public IFormFile Image {get; set;}
    }
}