namespace Api.CopilotDemo.Service;

public class SearchService : ISearchService
{
    private readonly SearchClient _searchClient;
    private readonly Config _config;

    public SearchService(IOptions<Config> options)
    {
        _config = options?.Value ?? throw new ArgumentNullException(nameof(options));

        // Initialize Azure Cognitive Search clients  
        var searchCredential = new AzureKeyCredential(_config.SearchKey);
        var indexClient = new SearchIndexClient(new Uri(_config.SearchServiceUrl), searchCredential);
        _searchClient = indexClient.GetSearchClient(_config.SearchIndexName);
    }

    public async Task<SemanticSearchResponse?> GetSemanticSearchContextAsync(string query, CancellationToken cancellationToken = default)
    {
        // Set searching options
        var searchOptions = new SearchOptions
        {
            QueryType = SearchQueryType.Semantic,
            SemanticConfigurationName = _config.SearchIndexSemanticConfigurationName,
            QueryAnswer = QueryAnswerType.Extractive
        };

        // Do a semantic search
        SearchResults<SearchDocument> response = await _searchClient.SearchAsync<SearchDocument>(query, searchOptions, cancellationToken);
        
        // Get search result
        await foreach (SearchResult<SearchDocument> result in response.GetResultsAsync())
        {
            // Result is not relevant enough
            if (result.RerankerScore < 2) continue;

            // Shape into a response
            return new SemanticSearchResponse
            {
                Content = response.Answers.FirstOrDefault()?.Text ?? "",
                FilePath = result.Document["Title"].ToString() ?? "",
                Url = result.Document["Path"].ToString() ?? "",
                Query = query
            };
        }

        // No context found
        return null;
    }
}
