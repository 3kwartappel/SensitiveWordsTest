namespace SensitiveWordsService.API.DTOs
{
    public class SanitizeResponseDto
    {
        public string SanitizedText { get; set; } = string.Empty;
        public string OriginalText { get; set; } = string.Empty;
    }
} 