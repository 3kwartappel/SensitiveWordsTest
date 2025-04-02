using System;

namespace SensitiveWordsService.Core.Models
{
    public class SensitiveWord
    {
        public int Id { get; set; }
        public string Word { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public SensitiveWord()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
} 