namespace Api.CopilotDemo;

public class Config
{
    public string SystemMessage { get; set; } = string.Empty;
    public string NoContextSystemMessage { get; set; } = string.Empty;

    public string SearchServiceUrl { get; set; } = string.Empty;
    public string SearchKey { get; set; } = string.Empty;
    public string SearchIndexName { get; set; } = string.Empty;
    public string SearchIndexSemanticConfigurationName { get; set; } = string.Empty;

    public string AzureOpenAiResourceUrl { get; set; } = string.Empty;
    public string AzureOpenAiKey { get; set; } = string.Empty;
    public int MaxTokens { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public float Temperature { get; set; }
}
