using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SensitiveWordsService.Core.Interfaces;
using SensitiveWordsService.Core.Models;

namespace SensitiveWordsService.Core.Services
{
    public class SensitiveWordService : ISensitiveWordService
    {
        private readonly ISensitiveWordRepository _repository;
        private HashSet<string> _activeWordsCache;
        private DateTime _lastCacheUpdate;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        public SensitiveWordService(ISensitiveWordRepository repository)
        {
            _repository = repository;
            _activeWordsCache = new HashSet<string>();
            _lastCacheUpdate = DateTime.MinValue;
        }

        public async Task<IEnumerable<SensitiveWord>> GetAllWordsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<SensitiveWord> GetWordByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<SensitiveWord> CreateWordAsync(SensitiveWord word)
        {
            return await _repository.CreateAsync(word);
        }

        public async Task<bool> UpdateWordAsync(SensitiveWord word)
        {
            var result = await _repository.UpdateAsync(word);
            if (result)
            {
                await RefreshCacheAsync();
            }
            return result;
        }

        public async Task<bool> DeleteWordAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result)
            {
                await RefreshCacheAsync();
            }
            return result;
        }

        public async Task<string> SanitizeTextAsync(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            await EnsureCacheIsValidAsync();
            var words = input.Split(' ');
            var sanitizedWords = words.Select(word => 
                _activeWordsCache.Contains(word.ToUpper()) ? new string('*', word.Length) : word);
            
            return string.Join(" ", sanitizedWords);
        }

        private async Task EnsureCacheIsValidAsync()
        {
            if (DateTime.UtcNow - _lastCacheUpdate > _cacheDuration)
            {
                await RefreshCacheAsync();
            }
        }

        private async Task RefreshCacheAsync()
        {
            var words = await _repository.GetActiveWordsAsync();
            _activeWordsCache = new HashSet<string>(words.Select(w => w.Word.ToUpper()));
            _lastCacheUpdate = DateTime.UtcNow;
        }
    }
} 