﻿using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Data.Dto.Departments
{
    public class UpdateDepartmentDto
    {
        [Required(ErrorMessage = "Department name is required")]
        public string DepartmentName { get; set; }
    }
}