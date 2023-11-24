namespace Api.CopilotDemo.Service;

public class AIService : IAIService
{
    private readonly OpenAIClient _openAIClient;
    private readonly Config _config;

    public AIService(IOptions<Config> options)
    {
        _config = options?.Value ?? throw new ArgumentNullException(nameof(options));

        // Initialize OpenAI client  
        var credential = new AzureKeyCredential(_config.AzureOpenAiKey);
        _openAIClient = new OpenAIClient(new Uri(_config.AzureOpenAiResourceUrl), credential);
    }

    public async Task<ChatResponse> GetAIResponseAsync(SemanticSearchResponse? context,
                                                       IEnumerable<ChatContext> chatContext,
                                                       CancellationToken cancellationToken = default)
    {
        // Set system message
        var options = new ChatCompletionsOptions();
        options.Messages.Add(new ChatMessage(ChatRole.System, _config.SystemMessage));

        // Add chat conversation
        foreach (var chat in chatContext)
        {
            options.Messages.Add(new ChatMessage(chat.Role, chat.Message));
        }

        if (context != null)
        {
            // Add our context
            options.Messages.Add(new ChatMessage(ChatRole.System, context.Content));

            // Add users question
            options.Messages.Add(new ChatMessage(ChatRole.User, context.Query));
        }
        else
        {
            // Add NoContextSystemMessage
            options.Messages.Add(new ChatMessage(ChatRole.System, _config.NoContextSystemMessage));
        }

        // How creative do we need it to be
        options.Temperature = _config.Temperature;

        // Max tokens to return as response
        options.MaxTokens = _config.MaxTokens;

        // Call the LLM
        var result = await _openAIClient.GetChatCompletionsAsync(_config.ModelName, options, cancellationToken);
        
        // Select the first response
        var responseText = result.Value.Choices.FirstOrDefault()?.Message.Content;

        return new ChatResponse
        {
            Message = responseText ?? "Error",
            Context = context?.Content,
            FileName = context?.FilePath,
            FileLocation = context?.Url
        };
    }
}
