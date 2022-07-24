using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using GMS.Data.Models;

namespace GMS.Data.Context
{
    public class GMSUser : IdentityUser
    {
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(40)]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender {get; set;}
    }
}