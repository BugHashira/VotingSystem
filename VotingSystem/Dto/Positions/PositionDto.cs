﻿using System.ComponentModel.DataAnnotations.Schema;

namespace VotingSystem.Dto.Positions
{
    public class PositionDto
    {
        public Guid Id { get; set; }
        public string PositionName { get; set; }
        public string PositionDescription { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }


    public class SelectPositionDto
    {
        public Guid Id { get; set; }
        public string PositionName { get; set; }
    }
}
