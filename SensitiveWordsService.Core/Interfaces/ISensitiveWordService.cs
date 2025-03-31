using System.Collections.Generic;
using System.Threading.Tasks;
using SensitiveWordsService.Core.Models;

namespace SensitiveWordsService.Core.Interfaces
{
    public interface ISensitiveWordService
    {
        Task<IEnumerable<SensitiveWord>> GetAllWordsAsync();
        Task<SensitiveWord> GetWordByIdAsync(int id);
        Task<SensitiveWord> CreateWordAsync(SensitiveWord word);
        Task<bool> UpdateWordAsync(SensitiveWord word);
        Task<bool> DeleteWordAsync(int id);
        Task<string> SanitizeTextAsync(string input);
    }
} 