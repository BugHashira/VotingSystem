using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Data.Dto.Position
{
    public class CreatePositionDto
    {
        [Required(ErrorMessage = "Position Description is required")]
        public string PositionDescription { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
    }
}