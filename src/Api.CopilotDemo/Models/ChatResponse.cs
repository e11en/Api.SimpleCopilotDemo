namespace Api.CopilotDemo.Models;

public class ChatResponse
{
    public string Message { get; set; } = string.Empty;

    public string? FileName { get; set; }

    public string? FileLocation { get; set; }

    public string? Context { get; set; }
}
