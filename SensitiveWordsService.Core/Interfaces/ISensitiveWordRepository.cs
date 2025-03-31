using System.Collections.Generic;
using System.Threading.Tasks;
using SensitiveWordsService.Core.Models;

namespace SensitiveWordsService.Core.Interfaces
{
    public interface ISensitiveWordRepository
    {
        Task<IEnumerable<SensitiveWord>> GetAllAsync();
        Task<SensitiveWord> GetByIdAsync(int id);
        Task<SensitiveWord> CreateAsync(SensitiveWord word);
        Task<bool> UpdateAsync(SensitiveWord word);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<SensitiveWord>> GetActiveWordsAsync();
    }
} 