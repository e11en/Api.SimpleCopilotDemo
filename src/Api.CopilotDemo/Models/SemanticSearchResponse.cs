namespace Api.CopilotDemo.Models;

public class SemanticSearchResponse
{
    public string Query { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
