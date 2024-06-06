using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Models
{
    public class User : IdentityUser
    {
        public string MatricNumber { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int CollegeId { get; set; }
        public College College { get; set; }
    }
}


