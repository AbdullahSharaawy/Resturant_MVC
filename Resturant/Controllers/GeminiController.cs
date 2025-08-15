using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Resturant_BLL.AppSettingsSections;
using Resturant_BLL.ImplementServices;
using System.Text;
using System.Text.Json;

namespace Resturant_PL.Controllers
{
    public class GeminiController : Controller
    {
        private readonly GeminiSettings _GS;

        public GeminiController(IOptions<GeminiSettings> gS)
        {
            _GS = gS.Value;
        }

        public IActionResult Index()
        {
            return PartialView("_Gemini");
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage()
        {
            try
            {
                // Read raw JSON string from request body
                using var reader = new StreamReader(Request.Body);
                var body = await reader.ReadToEndAsync();

                // Parse JSON to get "message" property
                using var doc = JsonDocument.Parse(body);
                var userMessage = doc.RootElement.GetProperty("message").GetString();

                // Call Gemini API
                string reply = await SendToGemini(userMessage);

                return Json(new
                {
                    success = true,
                    answer = reply,
                    timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    answer = "Error: " + ex.Message,
                    timestamp = DateTime.Now
                });
            }
        }
        private async Task<string> SendToGemini(string message)
        {
            var apiKey = _GS.ApiKey; // Replace with your actual key
            var url = _GS.Url;
            var body = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new { text = message }
                }
            }
        }
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);
            var response = await client.PostAsync(url, content);
            var jsonString = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(jsonString);
            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text ?? "No response";
        }
    }
}
