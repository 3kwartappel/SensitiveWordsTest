using System.ComponentModel.DataAnnotations;

namespace SensitiveWordsService.API.DTOs
{
    public class SanitizeRequestDto
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Text { get; set; } = string.Empty;
    }
} 