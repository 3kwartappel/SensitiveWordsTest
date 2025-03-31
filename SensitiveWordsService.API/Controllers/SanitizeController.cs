using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SensitiveWordsService.API.DTOs;
using SensitiveWordsService.Core.Interfaces;

namespace SensitiveWordsService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SanitizeController : ControllerBase
    {
        private readonly ISensitiveWordService _service;
        private readonly ILogger<SanitizeController> _logger;

        public SanitizeController(
            ISensitiveWordService service,
            ILogger<SanitizeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SanitizeResponseDto), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SanitizeResponseDto>> Sanitize([FromBody] SanitizeRequestDto request)
        {
            var sanitizedText = await _service.SanitizeTextAsync(request.Text);
            return Ok(new SanitizeResponseDto
            {
                OriginalText = request.Text,
                SanitizedText = sanitizedText
            });
        }
    }
} 