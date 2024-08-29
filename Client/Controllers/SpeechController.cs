using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Client.Controllers
{
  

    [ApiController]
    [Route("api/[controller]")]
    public class SpeechController : ControllerBase
    {
        private readonly SpeechToTextService _speechToTextService;

        public SpeechController(SpeechToTextService speechToTextService)
        {
            _speechToTextService = speechToTextService;
        }

        [HttpPost("recognize")]
        public async Task<IActionResult> RecognizeSpeech([FromForm] IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
            {
                return BadRequest("Invalid audio file");
            }

            using (var stream = new MemoryStream())
            {
                await audioFile.CopyToAsync(stream);
                var audioBytes = stream.ToArray();
                var result = _speechToTextService.Recognize(audioBytes);
                return Ok(result);
            }
        }
    }

}
