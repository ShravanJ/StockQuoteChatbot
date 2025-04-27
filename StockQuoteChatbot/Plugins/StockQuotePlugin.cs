using System.ComponentModel;
using Microsoft.SemanticKernel;
using TwelveDataSharp;
using TwelveDataSharp.Interfaces;

namespace StockQuoteChatbot.Plugins;

public class StockQuotePlugin
{
    private readonly ITwelveDataClient _twelveDataClient;
    public StockQuotePlugin()
    {
        // TODO: Replace API key with your own
        _twelveDataClient = new TwelveDataClient("", new HttpClient());
    }

    [KernelFunction("get_real_time_price")]
    [Description("Returns the real time price for a symbol")]
    public async Task<double> GetPriceAsync(string symbol)
    {
        var quote = await _twelveDataClient.GetRealTimePriceAsync(symbol);
        return quote.Price;
    }

    [KernelFunction("get_closing_price")]
    [Description("Returns the previous closing price for a symbol")]
    public async Task<double> GetClosingPriceAsync(string symbol)
    {
        var quote = await _twelveDataClient.GetQuoteAsync(symbol);
        return quote.Close;
    }

    [KernelFunction("get_opening_price")]
    [Description("Returns the opening price for a symbol")]
    public async Task<double> GetOpeningPriceAsync(string symbol)
    {
        var quote = await _twelveDataClient.GetQuoteAsync(symbol);
        return quote.Open;
    }
}