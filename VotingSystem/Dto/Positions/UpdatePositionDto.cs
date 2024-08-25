using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.Positions
{
    public class UpdatePositionDto
    {
        [Required(ErrorMessage = "Position Description is required")]
        public string PositionDescription { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Position Name is required")]
        public string PositionName { get; set; }
    }
}
