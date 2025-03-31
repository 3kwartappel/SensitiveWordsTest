using System;

namespace SensitiveWordsService.API.DTOs
{
    public class SensitiveWordDto
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
} 