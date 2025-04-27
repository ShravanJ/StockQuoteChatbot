using StockQuoteChatbot.Services;

namespace StockQuoteChatbot;

class Program
{
    static async Task Main(string[] args)
    {
        var chatService = new ChatService();
        
        while (true)
        {
            Console.Write("User> ");
            var input = Console.ReadLine();
            if (input.ToLowerInvariant() == "exit")
            {
                break;
            }
            var response = await chatService.GetResponse(input);
            foreach (var responseMessage in response)
            {
                Console.WriteLine($"Chatbot> {responseMessage}");
            }
        }
    }
}