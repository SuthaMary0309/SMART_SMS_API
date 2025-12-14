using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<ChatController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        // Accepts JSON like: { "message": "hi" }
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequest? request)
        {
            // Validate request
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new { message = "Message is required" });
            }

            // Try to get API key from environment variable first, then configuration
            var apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY") 
                        ?? _configuration["Gemini:ApiKey"];
            var model = _configuration["Gemini:Model"] ?? "gemini-pro";

            if (string.IsNullOrEmpty(apiKey) || 
                apiKey == "YOUR_GOOGLE_CLOUD_API_KEY" || 
                apiKey.StartsWith("YOUR_"))
            {
                _logger.LogError("Gemini API key is missing or not configured");
                return BadRequest(new { 
                    message = "Gemini API key is not configured",
                    instructions = new[] {
                        "Option 1: Add to appsettings.json or appsettings.Development.json:",
                        "  \"Gemini\": { \"ApiKey\": \"YOUR_ACTUAL_KEY_HERE\" }",
                        "",
                        "Option 2: Set environment variable:",
                        "  GEMINI_API_KEY=your_actual_key_here",
                        "",
                        "Get your API key from: https://makersuite.google.com/app/apikey"
                    }
                });
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                
                // System instruction for EDU Link school management system AI assistant
                var systemInstruction = "You are a friendly and helpful AI assistant for EDU Link, a Sri Lankan school management system. " +
                    "Always respond in a warm, friendly manner. Your responses MUST be exactly 2 lines - simple, clear, and easy to understand. " +
                    "Use school-level language appropriate for Sri Lankan students, teachers, and parents. " +
                    "When referring to the school system, use the name 'EDU Link'. " +
                    "Explain concepts in the simplest terms suitable for the Sri Lankan education system. " +
                    "Be aware of Sri Lankan school culture, curriculum (O/L, A/L), and educational practices. " +
                    "Keep every answer concise, friendly, and limited to exactly 2 lines maximum.";

                // Gemini API uses query parameter for API key, not Bearer token
                var url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

                // Gemini API v1 format with system instruction
                var requestBody = new
                {
                    systemInstruction = new
                    {
                        parts = new[]
                        {
                            new { text = systemInstruction }
                        }
                    },
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = request.Message }
                            }
                        }
                    }
                };

                var jsonContent = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                _logger.LogInformation($"Sending request to Gemini API: {url}");
                var response = await client.PostAsync(url, content);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Gemini API error: {response.StatusCode} - {responseContent}");
                    
                    // Try to parse the error response for better user feedback
                    string errorMessage = "Failed to get response from AI";
                    try
                    {
                        using var errorDoc = JsonDocument.Parse(responseContent);
                        if (errorDoc.RootElement.TryGetProperty("error", out var errorObj))
                        {
                            if (errorObj.TryGetProperty("message", out var errorMsg))
                            {
                                errorMessage = errorMsg.GetString() ?? errorMessage;
                            }
                            else if (errorObj.TryGetProperty("error", out var nestedError) &&
                                     nestedError.TryGetProperty("message", out var nestedMsg))
                            {
                                errorMessage = nestedMsg.GetString() ?? errorMessage;
                            }
                        }
                    }
                    catch
                    {
                        // If parsing fails, use the raw response
                        errorMessage = responseContent;
                    }

                    // Provide helpful message for API key errors
                    if (errorMessage.Contains("API key") || errorMessage.Contains("API_KEY"))
                    {
                        errorMessage = "Invalid or missing Gemini API key. Please check your appsettings.json configuration.";
                    }

                    return StatusCode((int)response.StatusCode, new { 
                        message = errorMessage,
                        hint = "Make sure your Gemini API key is valid and properly configured in appsettings.json"
                    });
                }

                // Parse Gemini API response
                using var doc = JsonDocument.Parse(responseContent);
                string aiResponse = string.Empty;

                if (doc.RootElement.TryGetProperty("candidates", out var candidates) && 
                    candidates.GetArrayLength() > 0)
                {
                    var firstCandidate = candidates[0];
                    if (firstCandidate.TryGetProperty("content", out var contentElement) &&
                        contentElement.TryGetProperty("parts", out var parts) &&
                        parts.GetArrayLength() > 0)
                    {
                        var firstPart = parts[0];
                        if (firstPart.TryGetProperty("text", out var textElement))
                        {
                            aiResponse = textElement.GetString() ?? string.Empty;
                        }
                    }
                }

                if (string.IsNullOrEmpty(aiResponse))
                {
                    _logger.LogWarning("Empty response from Gemini API");
                    return StatusCode(500, new { message = "No response from AI" });
                }

                return Ok(new { response = aiResponse });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Gemini API");
                return StatusCode(500, new { message = "An error occurred while processing your request", error = ex.Message });
            }
        }
    }

    public class ChatRequest
    {
        [Required(ErrorMessage = "Message is required")]
        [JsonPropertyName("message")] // Matches Angular JSON
        public string Message { get; set; } = string.Empty;
    }
}

