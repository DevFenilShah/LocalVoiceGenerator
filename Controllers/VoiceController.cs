using Microsoft.AspNetCore.Mvc;
using LocalVoiceGenerator.Models;
using LocalVoiceGenerator.Services;
using System;
using System.Threading.Tasks;

namespace LocalVoiceGenerator.Controllers
{
    public class VoiceController : Controller
    {
        private readonly IVoiceService _voiceService;

        public VoiceController(IVoiceService voiceService)
        {
            _voiceService = voiceService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Generate([FromBody] VoiceGenerationRequest request)
        {
            try
            {
                // Validate the request
                if (string.IsNullOrWhiteSpace(request?.Text))
                {
                    return Json(new { success = false, error = "Please enter some text to convert." });
                }

                if (request.Text.Length > 5000)
                {
                    return Json(new { success = false, error = "Text exceeds maximum length of 5000 characters." });
                }

                if (request.SpeakingRate < 0.5f || request.SpeakingRate > 2.0f)
                {
                    return Json(new { success = false, error = "Speaking rate must be between 0.5 and 2.0." });
                }

                // Generate the voice
                var audioBytes = await _voiceService.GenerateVoiceAsync(
                    request.Text,
                    request.LanguageCode,
                    request.VoiceType,
                    request.Gender,
                    request.SpeakingRate
                );

                // Return as file download
                return File(audioBytes, "audio/mpeg", "voiceover.mp3");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Preview([FromBody] VoiceGenerationRequest request)
        {
            try
            {
                // Validate the request
                if (string.IsNullOrWhiteSpace(request?.Text))
                {
                    return Json(new { success = false, error = "Please enter some text to convert." });
                }

                if (request.Text.Length > 5000)
                {
                    return Json(new { success = false, error = "Text exceeds maximum length of 5000 characters." });
                }

                if (request.SpeakingRate < 0.5f || request.SpeakingRate > 2.0f)
                {
                    return Json(new { success = false, error = "Speaking rate must be between 0.5 and 2.0." });
                }

                // Generate the voice
                var audioBytes = await _voiceService.GenerateVoiceAsync(
                    request.Text,
                    request.LanguageCode,
                    request.VoiceType,
                    request.Gender,
                    request.SpeakingRate
                );

                // Convert to base64 for inline preview
                var base64Audio = Convert.ToBase64String(audioBytes);
                return Json(new { success = true, audio = base64Audio });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
