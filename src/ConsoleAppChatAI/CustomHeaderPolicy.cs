using Microsoft.Extensions.Configuration;
using System.ClientModel.Primitives;

namespace ConsoleAppChatAI;

/// <summary>
/// Política personalizada para adicionar headers customizados às requisições HTTP
/// </summary>
public class CustomHeaderPolicy : PipelinePolicy
{
    private readonly IConfiguration _configuration;

    public CustomHeaderPolicy(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public override void Process(PipelineMessage message, IReadOnlyList<PipelinePolicy> pipeline, int currentIndex)
    {
        AddCustomHeaders(message);
        ProcessNext(message, pipeline, currentIndex);
    }

    public override async ValueTask ProcessAsync(PipelineMessage message, IReadOnlyList<PipelinePolicy> pipeline, int currentIndex)
    {
        AddCustomHeaders(message);
        await ProcessNextAsync(message, pipeline, currentIndex).ConfigureAwait(false);
    }

    private void AddCustomHeaders(PipelineMessage message)
    {
        message.Request.Headers.Add("apikey", _configuration["APISIX:ApiKey"]!);
    }
}
