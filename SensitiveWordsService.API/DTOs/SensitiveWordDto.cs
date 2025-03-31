using System;
using System.ComponentModel.DataAnnotations;

namespace SensitiveWordsService.API.DTOs
{
    public class SensitiveWordDto
    {
        public int Id { get; set; }
        [Required]
        public string Word { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
} 