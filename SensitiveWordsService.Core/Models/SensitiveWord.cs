using System;

namespace SensitiveWordsService.Core.Models
{
    public class SensitiveWord
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
} 