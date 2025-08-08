using Microsoft.AspNetCore.Mvc;

public class GeminiController : Controller
{
    private readonly GeminiService _geminiService;

    public GeminiController(IConfiguration configuration)
    {
        var apiKey = configuration["Gemini:ApiKey"];
        _geminiService = new GeminiService(apiKey);
    }

    public async Task<IActionResult> Ask(string prompt)
    {
        var reply = await _geminiService.AskGeminiAsync(prompt);
        return Content(reply);
    }
}
