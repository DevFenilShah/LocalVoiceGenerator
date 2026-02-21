namespace LocalVoiceGenerator.Models
{
    public class VoiceGenerationRequest
    {
        public string? Text { get; set; }
        public string LanguageCode { get; set; } = "en-IN";
        public string VoiceType { get; set; } = "standard";
        public string Gender { get; set; } = "male";
        public float SpeakingRate { get; set; } = 1.0f;
    }
}
