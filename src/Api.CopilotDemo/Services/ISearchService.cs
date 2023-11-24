namespace Api.CopilotDemo.Service;

public interface ISearchService
{
    public Task<SemanticSearchResponse?> GetSemanticSearchContextAsync(string query, CancellationToken cancellationToken = default);
}
