using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Data.Entities
{
    public class User : IdentityUser
    {
        public Guid MatricNumber { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public Guid CollegeId { get; set; }
        public College College { get; set; }
    }
}


