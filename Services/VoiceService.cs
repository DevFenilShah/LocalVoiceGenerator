using Google.Cloud.TextToSpeech.V1;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LocalVoiceGenerator.Services
{
    public interface IVoiceService
    {
        Task<byte[]> GenerateVoiceAsync(string text, string languageCode, string voiceType, string gender, float speakingRate);
    }

    public class VoiceService : IVoiceService
    {
        private readonly TextToSpeechClient _client;
        private const int MaxCharacters = 5000;

        public VoiceService()
        {
            _client = TextToSpeechClient.Create();
        }

        public async Task<byte[]> GenerateVoiceAsync(string text, string languageCode, string voiceType, string gender, float speakingRate)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Text cannot be empty.");
            }

            if (text.Length > MaxCharacters)
            {
                throw new ArgumentException($"Text exceeds maximum length of {MaxCharacters} characters.");
            }

            // Validate speaking rate
            if (speakingRate < 0.5f || speakingRate > 2.0f)
            {
                throw new ArgumentException("Speaking rate must be between 0.5 and 2.0.");
            }

            try
            {
                // Set the text input to be synthesized
                var input = new SynthesisInput
                {
                    Text = text
                };

                // Determine voice name based on language, type, and gender
                var voiceName = GetVoiceName(languageCode, voiceType, gender);

                // Build the voice request
                var voice = new VoiceSelectionParams
                {
                    LanguageCode = languageCode,
                    Name = voiceName
                };

                // Select the type of audio file you want returned
                var audioConfig = new AudioConfig
                {
                    AudioEncoding = AudioEncoding.Mp3,
                    SpeakingRate = speakingRate
                };

                // Perform the text-to-speech request
                var response = await _client.SynthesizeSpeechAsync(new SynthesizeSpeechRequest
                {
                    Input = input,
                    Voice = voice,
                    AudioConfig = audioConfig
                });

                // Write the response to an output file
                return response.AudioContent.ToByteArray();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to generate voice. Please check your Google Cloud credentials and API key.", ex);
            }
        }

        private string GetVoiceName(string languageCode, string voiceType, string gender)
        {
            if (voiceType.ToLower() != "wavenet" && voiceType.ToLower() != "standard")
            {
                voiceType = "wavenet";
            }

            // Determine voice based on language, type, and gender
            var voicePrefix = voiceType.ToLower() == "wavenet" ? "Wavenet" : "Standard";

            return languageCode switch
            {
                // Hindi (India) - Male: A, Female: D
                "hi-IN" => gender.ToLower() == "male" ? $"hi-IN-{voicePrefix}-A" : $"hi-IN-{voicePrefix}-D",

                // English (India) - Male: B, Female: A
                "en-IN" => gender.ToLower() == "male" ? $"en-IN-{voicePrefix}-B" : $"en-IN-{voicePrefix}-A",

                // Gujarati (India) - Male: B, Female: A (Note: Only Standard available, Wavenet will fall back to Standard)
                "gu-IN" => gender.ToLower() == "male" ? $"gu-IN-Standard-B" : $"gu-IN-Standard-A",

                // Default to English
                _ => $"en-IN-{voicePrefix}-B"
            };
        }
    }
}
