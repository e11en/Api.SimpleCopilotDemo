namespace Api.CopilotDemo.Models;

public class ChatRequest
{
    public string Input { get; set; } = string.Empty;
    public IEnumerable<ChatContext> ChatHistory { get; set; } = Enumerable.Empty<ChatContext>();
}