﻿using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Dto.Elections
{
    public class UpdateElectionDto
    {
        [Required(ErrorMessage = "Election name is required")]
        public string ElectionName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string DescriptionName { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }
    }
}
