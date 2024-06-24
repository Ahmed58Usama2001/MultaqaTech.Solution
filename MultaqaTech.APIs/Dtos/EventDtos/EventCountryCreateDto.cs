﻿namespace MultaqaTech.APIs.Dtos.EventDtos
{
    public class EventCountryCreateDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "The Category Name must be less than 100 characters long.")]
        [MinLength(3, ErrorMessage = "The Category Name must be at least 3 characters long.")]
        public string Name { get; set; }
    }
}
