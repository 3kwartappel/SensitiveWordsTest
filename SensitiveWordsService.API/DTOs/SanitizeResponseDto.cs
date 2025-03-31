namespace SensitiveWordsService.API.DTOs
{
    public class SanitizeResponseDto
    {
        public string SanitizedText { get; set; }
        public string OriginalText { get; set; }
    }
} 