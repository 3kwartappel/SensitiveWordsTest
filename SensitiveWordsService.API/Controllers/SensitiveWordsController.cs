using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SensitiveWordsService.API.DTOs;
using SensitiveWordsService.Core.Interfaces;
using SensitiveWordsService.Core.Models;

namespace SensitiveWordsService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensitiveWordsController : ControllerBase
    {
        private readonly ISensitiveWordService _service;
        private readonly ILogger<SensitiveWordsController> _logger;

        public SensitiveWordsController(
            ISensitiveWordService service,
            ILogger<SensitiveWordsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SensitiveWordDto>), 200)]
        public async Task<ActionResult<IEnumerable<SensitiveWordDto>>> GetAll()
        {
            var words = await _service.GetAllWordsAsync();
            return Ok(MapToDtos(words));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SensitiveWordDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SensitiveWordDto>> GetById(int id)
        {
            var word = await _service.GetWordByIdAsync(id);
            if (word == null)
            {
                return NotFound();
            }

            return Ok(MapToDto(word));
        }

        [HttpPost]
        [ProducesResponseType(typeof(SensitiveWordDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SensitiveWordDto>> Create([FromBody] SensitiveWordDto dto)
        {
            var word = MapToModel(dto);
            var created = await _service.CreateWordAsync(word);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, MapToDto(created));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] SensitiveWordDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var word = MapToModel(dto);
            var result = await _service.UpdateWordAsync(word);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteWordAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static SensitiveWordDto MapToDto(SensitiveWord word)
        {
            return new SensitiveWordDto
            {
                Id = word.Id,
                Word = word.Word,
                CreatedAt = word.CreatedAt,
                UpdatedAt = word.UpdatedAt,
                IsActive = word.IsActive
            };
        }

        private static IEnumerable<SensitiveWordDto> MapToDtos(IEnumerable<SensitiveWord> words)
        {
            foreach (var word in words)
            {
                yield return MapToDto(word);
            }
        }

        private static SensitiveWord MapToModel(SensitiveWordDto dto)
        {
            return new SensitiveWord
            {
                Id = dto.Id,
                Word = dto.Word,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                IsActive = dto.IsActive
            };
        }
    }
} 