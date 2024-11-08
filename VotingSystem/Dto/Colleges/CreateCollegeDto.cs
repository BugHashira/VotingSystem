using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.Colleges
{
    public class CreateCollegeDto
    {
        [Required(AllowEmptyStrings =false,ErrorMessage = "College name is required")]
        public string CollegeName { get; set; }
    }
}