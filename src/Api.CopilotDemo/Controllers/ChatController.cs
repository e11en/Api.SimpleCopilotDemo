namespace Api.CopilotDemo.Controllers;

[ApiController]
[Route("/chat")]
[Produces("application/json")]
public class ChatController : ControllerBase
{
    private readonly IAIService _aiService;
    private readonly ISearchService _searchService;

    public ChatController(IAIService aiService, ISearchService searchService)
    {
        _aiService = aiService ?? throw new ArgumentNullException(nameof(aiService), $"{nameof(aiService)} parameter is null.");
        _searchService = searchService ?? throw new ArgumentNullException(nameof(searchService), $"{nameof(searchService)} parameter is null.");
    }

    [HttpPost]
    public async Task<IActionResult> Chat([FromBody] ChatRequest request)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(request.Input))
            return BadRequest("No input provided");

        try
        {
            // Get context
            var context = await _searchService.GetSemanticSearchContextAsync(request.Input);
            
            // Answer question by LLM
            var result = await _aiService.GetAIResponseAsync(context, request.ChatHistory);

            // Return to the user
            return Ok(result);
        }
        catch
        {
            return Problem("Something went very wrong here.");
        }
    }
}