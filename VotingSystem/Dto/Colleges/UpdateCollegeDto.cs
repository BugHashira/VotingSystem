using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.Colleges
{
    public class UpdateCollegeDto
    {
        [Required(ErrorMessage = "College name is required")]
        public string CollegeName { get; set; }
    }
}
