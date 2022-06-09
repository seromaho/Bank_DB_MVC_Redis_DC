using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bank_DB_MVC_Redis_DC
{
    public static class Distributed_Cache_Extensions
    {
        public static async Task SetRecordAsync<Type>(
            this IDistributedCache distributedCache,
            string recordId,
            Type data,
            TimeSpan? absoluteExpirationTime = null,
            TimeSpan? unusedExpirationTime = null)
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpirationTime ?? TimeSpan.FromMinutes(60);
            options.SlidingExpiration = unusedExpirationTime;

            var jsonData = JsonSerializer.Serialize(data);
            await distributedCache.SetStringAsync(recordId, jsonData, options);
        }

        public static async Task<Type> GetRecordAsync<Type>(this IDistributedCache distributedCache, string recordId)
        {
            var jsonData = await distributedCache.GetStringAsync(recordId);

            if (jsonData is null)
            {
                return default(Type);
            }

            return JsonSerializer.Deserialize<Type>(jsonData);
        }
    }
}
