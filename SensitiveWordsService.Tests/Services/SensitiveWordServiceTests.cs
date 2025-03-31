using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SensitiveWordsService.Core.Interfaces;
using SensitiveWordsService.Core.Models;
using SensitiveWordsService.Core.Services;
using Xunit;

namespace SensitiveWordsService.Tests.Services
{
    public class SensitiveWordServiceTests
    {
        private readonly Mock<ISensitiveWordRepository> _repositoryMock;
        private readonly ISensitiveWordService _service;

        public SensitiveWordServiceTests()
        {
            _repositoryMock = new Mock<ISensitiveWordRepository>();
            _service = new SensitiveWordService(_repositoryMock.Object);
        }

        [Fact]
        public async Task SanitizeText_WithSensitiveWords_ReplacesWordsWithAsterisks()
        {
            // Arrange
            var sensitiveWords = new List<SensitiveWord>
            {
                new() { Id = 1, Word = "SELECT", IsActive = true },
                new() { Id = 2, Word = "FROM", IsActive = true }
            };

            _repositoryMock.Setup(r => r.GetActiveWordsAsync())
                .ReturnsAsync(sensitiveWords);

            // Act
            var result = await _service.SanitizeTextAsync("SELECT * FROM users");

            // Assert
            Assert.Equal("****** * **** users", result);
        }

        [Fact]
        public async Task SanitizeText_WithNoSensitiveWords_ReturnsOriginalText()
        {
            // Arrange
            var sensitiveWords = new List<SensitiveWord>
            {
                new() { Id = 1, Word = "SELECT", IsActive = true }
            };

            _repositoryMock.Setup(r => r.GetActiveWordsAsync())
                .ReturnsAsync(sensitiveWords);

            // Act
            var result = await _service.SanitizeTextAsync("Hello World");

            // Assert
            Assert.Equal("Hello World", result);
        }

        [Fact]
        public async Task SanitizeText_WithEmptyInput_ReturnsEmptyString()
        {
            // Act
            var result = await _service.SanitizeTextAsync("");

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public async Task SanitizeText_WithNullInput_ReturnsNull()
        {
            // Act
            var result = await _service.SanitizeTextAsync(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task SanitizeText_WithCaseInsensitiveWords_ReplacesAllCases()
        {
            // Arrange
            var sensitiveWords = new List<SensitiveWord>
            {
                new() { Id = 1, Word = "SELECT", IsActive = true }
            };

            _repositoryMock.Setup(r => r.GetActiveWordsAsync())
                .ReturnsAsync(sensitiveWords);

            // Act
            var result = await _service.SanitizeTextAsync("Select * from SELECT");

            // Assert
            Assert.Equal("****** * from ******", result);
        }
    }
} 