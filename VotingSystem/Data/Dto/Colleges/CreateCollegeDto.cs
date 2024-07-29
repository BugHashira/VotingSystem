using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Data.Dto.College
{
    public class CreateCollegeDto
    {
        [Required(ErrorMessage = "College name is required")]
        public string CollegeName { get; set; }
    }
}