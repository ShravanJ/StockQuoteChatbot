using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using StockQuoteChatbot.Plugins;

namespace StockQuoteChatbot.Services;

public class ChatService
{
    private ChatHistory history = [];
    private readonly IChatCompletionService _chatCompletionService;
    private readonly Kernel _kernel;
    public ChatService()
    {
        var kernelBuilder = Kernel.CreateBuilder();
        kernelBuilder.AddOpenAIChatCompletion(
            modelId: "phi2",
            apiKey: "API_KEY", // No API key is needed since we are running this model locally 
            endpoint: new Uri("http://127.0.0.1:1234/v1"), // Used to point to your service
            serviceId: "SERVICE_ID", // Optional; for targeting specific services within Semantic Kernel
            httpClient: new HttpClient() // Optional; for customizing HTTP client
        );
        kernelBuilder.Plugins.AddFromType<StockQuotePlugin>("StockQuotePlugin");
        
        _kernel = kernelBuilder.Build();
        
        _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
    }

    public async Task<List<string?>> GetResponse(string userMessage)
    {
        history.AddUserMessage(userMessage);
        
        // Explicitly register the functions to make sure the model uses the TwelveDataSharpClient for retrieving quotes 
        var realTimePriceFunction = _kernel.Plugins.GetFunction("StockQuotePlugin", "get_real_time_price");
        var closingPriceFunction = _kernel.Plugins.GetFunction("StockQuotePlugin", "get_closing_price");
        var openingPriceFunction = _kernel.Plugins.GetFunction("StockQuotePlugin", "get_opening_price");
        
        OpenAIPromptExecutionSettings promptExecutionSettings = new() 
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Required(functions: [realTimePriceFunction, closingPriceFunction, openingPriceFunction])
        };
        
        var response = await _chatCompletionService.GetChatMessageContentsAsync(history, kernel: _kernel, executionSettings: promptExecutionSettings);
        
        return response.Select(x => x.Content).ToList();
    }
}