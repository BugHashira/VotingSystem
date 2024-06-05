using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using VotingSystem.Models;

namespace VotingSystem.Data.Entities
{
    public class Position : BaseEntity
    {
        public string PositionName { get; set; }
        public string PositionDescription { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}

