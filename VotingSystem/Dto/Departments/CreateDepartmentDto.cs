using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.Departments
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Department name is required")]
        public string DepartmentName { get; set; }
    }
}