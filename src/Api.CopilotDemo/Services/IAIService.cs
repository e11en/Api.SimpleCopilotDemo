namespace Api.CopilotDemo.Service;

public interface IAIService
{
    public Task<ChatResponse> GetAIResponseAsync(SemanticSearchResponse? context, IEnumerable<ChatContext> chatContext, CancellationToken cancellationToken = default);
}
