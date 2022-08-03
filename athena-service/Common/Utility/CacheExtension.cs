using Microsoft.Extensions.Caching.Memory;
using AthenaService.Configuration;
using AthenaService.Domain.Base;

namespace AthenaService.Common.Utility
{

    /// <summary>
    /// Extension method for Memory Cache. The flow of the function is that storing in cache memory
    /// with key "Tenant Id" + tenantId (i.e "Tenant Id 1") and value is the connectionString of that tenant.
    /// And the function return a conectionString (string)
    /// </summary>
    public static class CacheExtension
    {
        public async static Task<string> GetOrCreateConnectionString(this IMemoryCache memoryCache, IConfiguration configuration,
                                                int tenantId/* TODO - implement 3 params: httpClient, database type, tenant id*/)
        {
            var cachedTimeMinutes = configuration.GetSection($"{nameof(CacheConfig)}").Get<CacheConfig>().TimeMinutes;

            return await memoryCache.GetOrCreateAsync(
                CommonConstants.TenantName + tenantId,
                async cacheEntry =>
                {
                    cacheEntry.AbsoluteExpiration = DateTime.UtcNow.AddMinutes(cachedTimeMinutes);

                    // TODO - Using Http Client to query Admin to take the connection string then return

                    return "Data Source=2LHZQN2;Initial Catalog=tuhngo-test-source;Integrated Security=True"; // hardcode
                });
        }
    }
}
