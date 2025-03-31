using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SensitiveWordsService.Core.Interfaces;
using SensitiveWordsService.Core.Models;

namespace SensitiveWordsService.Infrastructure.Repositories
{
    public class SensitiveWordRepository : ISensitiveWordRepository
    {
        private readonly string _connectionString;
        private const string TableName = "SensitiveWords";

        public SensitiveWordRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<SensitiveWord>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<SensitiveWord>($"SELECT * FROM {TableName}");
        }

        public async Task<SensitiveWord> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<SensitiveWord>(
                $"SELECT * FROM {TableName} WHERE Id = @Id",
                new { Id = id });
        }

        public async Task<SensitiveWord> CreateAsync(SensitiveWord word)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = $@"
                INSERT INTO {TableName} (Word, CreatedAt, IsActive)
                VALUES (@Word, @CreatedAt, @IsActive);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            word.CreatedAt = DateTime.UtcNow;
            word.Id = await connection.ExecuteScalarAsync<int>(sql, word);
            return word;
        }

        public async Task<bool> UpdateAsync(SensitiveWord word)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = $@"
                UPDATE {TableName}
                SET Word = @Word,
                    UpdatedAt = @UpdatedAt,
                    IsActive = @IsActive
                WHERE Id = @Id";

            word.UpdatedAt = DateTime.UtcNow;
            var rowsAffected = await connection.ExecuteAsync(sql, word);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var rowsAffected = await connection.ExecuteAsync(
                $"DELETE FROM {TableName} WHERE Id = @Id",
                new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<SensitiveWord>> GetActiveWordsAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<SensitiveWord>(
                $"SELECT * FROM {TableName} WHERE IsActive = 1");
        }
    }
} 