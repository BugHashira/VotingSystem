using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.Colleges
{
    public class CreateCollegeDto
    {
        [Required(ErrorMessage = "College name is required")]
        public string CollegeName { get; set; }
    }
}