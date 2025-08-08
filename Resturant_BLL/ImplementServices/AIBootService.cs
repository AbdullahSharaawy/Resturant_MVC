using GenerativeAI;
using Microsoft.Extensions.Configuration;

public class GeminiService
{
    private readonly GenerativeModel _model;

    public GeminiService(string apiKey)
    {
        _model = new GenerativeModel(
            model: "models/gemini-1.5-pro-latest",
            apiKey: apiKey
        );
    }

    public async Task<string> AskGeminiAsync(string prompt)
    {
        var result = await _model.GenerateContentAsync(prompt);
        return result?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text ?? "No response";
    }
}
