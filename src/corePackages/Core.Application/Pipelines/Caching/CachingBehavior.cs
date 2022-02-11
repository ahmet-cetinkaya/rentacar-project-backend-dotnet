using System.Text;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Core.Application.Pipelines.Caching;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICachableRequest
{
    private readonly IDistributedCache _cache;

    // private readonly ILogger _logger;
    // private readonly CacheSettings _settings;

    public CachingBehavior(IDistributedCache cache
        // ILogger logger,
        //CacheSettings settings
    )
    {
        _cache = cache;
        // _logger = logger;
        // _settings = settings;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
                                        RequestHandlerDelegate<TResponse> next)
    {
        TResponse response;
        if (request.BypassCache) return await next();

        async Task<TResponse> GetResponseAndAddToCache()
        {
            response = await next();
            TimeSpan? slidingExpiration = request.SlidingExpiration == null
                                              ? TimeSpan.FromHours(2) //todo: _settings.SlidingExpiration
                                              : request.SlidingExpiration;
            DistributedCacheEntryOptions cacheOptions = new() { SlidingExpiration = slidingExpiration };
            byte[] serializeData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
            await _cache.SetAsync(request.CacheKey, serializeData, cacheOptions, cancellationToken);
            return response;
        }

        byte[]? cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
        if (cachedResponse != null)
            response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
        // _logger.LogInformation($"Fetched from Cache -> {request.CacheKey}"); //todo
        else
            response = await GetResponseAndAddToCache();
        // _logger.LogInformation($"Added to Cache -> {request.CacheKey}");  //todo

        return response;
    }
}